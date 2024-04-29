using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs.UsersFavouriteDTOs;
using Services.IServices;

namespace Services.Implementations;

public sealed class UsersFavouriteService : IUsersFavouriteService
{
    private readonly IUsersFavouriteRepository _usersFavouriteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public UsersFavouriteService(IUsersFavouriteRepository usersFavouriteRepository, 
        IUserRepository userRepository, 
        IPropertyRepository propertyRepository, IMapper mapper)
    {
        _usersFavouriteRepository = usersFavouriteRepository;
        _userRepository = userRepository;
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddFavouriteAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }

        if (!await _propertyRepository.ExistsAsync(propertyId, cancellationToken))
        {
            return Results.NotFound("Property is not found");
        }
        
        if (await _usersFavouriteRepository.ExistsForUserAsync(userId, propertyId, cancellationToken))
        {
            return Results.BadRequest("Property is already in favourites");
        }
        
        var userFavourite = new UsersFavourite
        {
            UserId = userId,
            PropertyId = propertyId
        };

        var result = await _usersFavouriteRepository.AddAsync(userFavourite, cancellationToken);

        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Results.Ok();
    }

    public async Task<IResult> RemoveFavouriteAsync(Guid userId, int usersFavouriteId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        if (!await _usersFavouriteRepository.ExistsAsync(usersFavouriteId, cancellationToken))
        {
            return Results.NotFound("Property is not found");
        }
        
        if (!await _usersFavouriteRepository.DeleteAsync(usersFavouriteId, cancellationToken))
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> GetFavouritesAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var favourites = await _usersFavouriteRepository.GetForUserAsync(userId, cancellationToken);
        
        var favouritesListed = _mapper.Map<List<UsersFavouriteListedDto>>(favourites);
        
        return Results.Ok(favouritesListed);
    }
}