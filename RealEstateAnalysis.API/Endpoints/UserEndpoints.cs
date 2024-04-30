using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.UserDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints
{
    public static class UserEndpoints
    {
        public static WebApplication AddUserEndpoints(this WebApplication webApplication)
        {
            webApplication.MapPost($"/{RouteNameConstants.Users}/{RouteNameConstants.Register}",
                    RegisterUser)
                .AllowAnonymous()
                .Produces(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status400BadRequest)
                .Produces<int>(StatusCodes.Status500InternalServerError)
                .WithTags(nameof(UserEndpoints))
                .WithName(nameof(RegisterUser))
                .WithOpenApi();
            
            webApplication.MapPost($"/{RouteNameConstants.Users}/{RouteNameConstants.Login}",
                    LoginUser)
                .AllowAnonymous()
                .Produces<TokenDto>()
                .Produces<string>(StatusCodes.Status400BadRequest)
                .Produces<string>(StatusCodes.Status404NotFound)
                .WithTags(nameof(UserEndpoints))
                .WithName(nameof(LoginUser))
                .WithOpenApi();
            
            webApplication.MapGet($"/{RouteNameConstants.Users}/{RouteNameConstants.MyProfile}",
                    GetUserDetails)
                .RequireAuthorization()
                .Produces<UserDetailsDto>()
                .Produces<string>(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags(nameof(UserEndpoints))
                .WithName(nameof(GetUserDetails))
                .WithOpenApi();
            
            webApplication.MapPut($"/{RouteNameConstants.Users}/{RouteNameConstants.MyProfile}",
                    UpdateUserDetails)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status404NotFound)
                .Produces<int>(StatusCodes.Status500InternalServerError)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags(nameof(UserEndpoints))
                .WithName(nameof(UpdateUserDetails))
                .WithOpenApi();
            
            return webApplication;
        }

        private static async Task<IResult> RegisterUser([FromServices] IUserService userService,
            [FromBody] AddUserDto request, CancellationToken cancellationToken)
        {
            return await userService.RegisterAsync(request, cancellationToken);
        }
        
        private static async Task<IResult> LoginUser([FromServices] IUserService userService,
            [FromBody] UserLoginDto request, CancellationToken cancellationToken)
        {
            return await userService.LoginAsync(request, cancellationToken);
        }
        
        private static async Task<IResult> GetUserDetails([FromServices] IUserService userService,
            [FromServices] IHttpContextAccessor contextAccessor, CancellationToken cancellationToken)
        {
            if (!contextAccessor.TryGetUserId(out Guid userId))
            {
                return Results.Unauthorized();
            }
            
            return await userService.GetUserDetails(userId, cancellationToken);
        }
        
        private static async Task<IResult> UpdateUserDetails([FromServices] IUserService userService,
            [FromServices] IHttpContextAccessor contextAccessor,
            [FromBody] UpdateUserInfoDto request, CancellationToken cancellationToken)
        {
            if (!contextAccessor.TryGetUserId(out Guid userId))
            {
                return Results.Unauthorized();
            }
            
            return await userService.ChangeUserDetails(userId, request, cancellationToken);
        }
    }
}