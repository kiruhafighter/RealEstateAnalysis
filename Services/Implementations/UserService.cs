using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repositories;
using Services.DTOs.UserDTOs;
using Services.IServices;
using Services.Utils;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JWTOptions _jwtOptions;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,
        IOptions<JWTOptions> jwtOptions, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<IResult> RegisterAsync(AddUserDto addUserRequest, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(addUserRequest.Email, cancellationToken))
        {
            return Results.BadRequest("User already exists.");
        }

        if (!addUserRequest.Password.Equals(addUserRequest.ConfirmPassword))
        {
            return Results.BadRequest("Password is not equal to confirmation password");
        }
            
        var (hash, salt) = CreatePasswordHashAndSalt(addUserRequest.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = addUserRequest.Email,
            FirstName = addUserRequest.FirstName,
            LastName = addUserRequest.LastName,
            PasswordHash = hash,
            PasswordSalt = salt,
            RoleId = (int)Enums.Role.User 
        };
            
        var add = await _userRepository.AddAsync(user, cancellationToken);
            
        if (!add)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
            
        return Results.Ok();
    }
        
    public async Task<IResult> LoginAsync(UserLoginDto request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
        if (user is null)
        {
            return Results.NotFound("User with such email is not found");
        }
            
        if (!ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Results.BadRequest("Wrong password");
        }
            
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Role, user.Role!.RoleName)
        };
            
        var expiryTime = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpiresInMinutes);
        var token = JWTGenerator.GenerateToken(_jwtOptions, claims, expiryTime);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            
        return Results.Ok(new TokenDto
        {
            AccessToken = accessToken,
            AccessTokenExpiryTime = expiryTime
        });
    }

    public async Task<IResult> GetUserDetails(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            
        if (user is null)
        {
            return Results.NotFound("User is not found");
        }
            
        var userDetails = _mapper.Map<UserDetailsDto>(user);
            
        return Results.Ok(userDetails);
    }
        
    public async Task<IResult> ChangeUserDetails(Guid id, UpdateUserInfoDto updatedInfo, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(id, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
            
        var update = await _userRepository.UpdateUserInfoAsync(id, updatedInfo.Email, updatedInfo.FirstName,
            updatedInfo.LastName, cancellationToken);
            
        if (!update)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
            
        return Results.Ok();
    }
        
    private static (string Hash, string Salt) CreatePasswordHashAndSalt(string password)
    {
        using var hmac = new HMACSHA512();
            
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            
        return (Convert.ToBase64String(passwordHash), Convert.ToBase64String(passwordSalt));
    }
        
    private static bool ValidatePassword(string password, string hash, string salt)
    {
        var computedHash = GenerateHash(password, salt);
        return hash.Equals(computedHash);
    }
        
    private static string GenerateHash(string password, string salt)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(salt));
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(computedHash);
    }
}