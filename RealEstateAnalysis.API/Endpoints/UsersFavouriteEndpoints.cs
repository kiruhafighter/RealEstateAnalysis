using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.UsersFavouriteDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints;

internal static class UsersFavouriteEndpoints
{
    public static WebApplication AddUsersFavouriteEndpoints(this WebApplication webApplication)
    {
        webApplication.MapPost($"/{RouteNameConstants.UserFavourites}", AddUsersFavourite)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(UsersFavouriteEndpoints))
            .WithName(nameof(AddUsersFavourite))
            .WithOpenApi();
        
        webApplication.MapDelete($"/{RouteNameConstants.UserFavourites}/{{usersFavouriteId}}", RemoveUsersFavourite)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(UsersFavouriteEndpoints))
            .WithName(nameof(RemoveUsersFavourite))
            .WithOpenApi();
        
        webApplication.MapDelete($"/{RouteNameConstants.UserFavourites}/remove/{{propertyId}}", RemovePropertyFromFavourites)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(UsersFavouriteEndpoints))
            .WithName(nameof(RemovePropertyFromFavourites))
            .WithOpenApi();
        
        webApplication.MapGet($"/{RouteNameConstants.UserFavourites}", GetUsersFavourites)
            .RequireAuthorization()
            .Produces<IList<UsersFavouriteListedDto>>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(UsersFavouriteEndpoints))
            .WithName(nameof(GetUsersFavourites))
            .WithOpenApi();
        
        webApplication.MapGet($"{RouteNameConstants.UserFavourites}/exists/{{propertyId}}", CheckIfPropertyIsFavourite)
            .RequireAuthorization()
            .Produces<bool>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(UsersFavouriteEndpoints))
            .WithName(nameof(CheckIfPropertyIsFavourite))
            .WithOpenApi();

        return webApplication;
    }
    
    private static async Task<IResult> AddUsersFavourite([FromServices] IUsersFavouriteService usersFavouriteService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] Guid propertyId, 
        CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await usersFavouriteService.AddFavouriteAsync(userId, propertyId, cancellationToken);
    }
    
    private static async Task<IResult> RemoveUsersFavourite([FromServices] IUsersFavouriteService usersFavouriteService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] int usersFavouriteId, 
        CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await usersFavouriteService.RemoveFavouriteAsync(userId, usersFavouriteId, cancellationToken);
    }
    
    private static async Task<IResult> RemovePropertyFromFavourites([FromServices] IUsersFavouriteService usersFavouriteService,
        [FromServices] IHttpContextAccessor contextAccessor, 
        [FromRoute] Guid propertyId,
        CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await usersFavouriteService.RemovePropertyFromFavouritesAsync(userId, propertyId, cancellationToken);
    }
    
    private static async Task<IResult> GetUsersFavourites([FromServices] IUsersFavouriteService usersFavouriteService,
        [FromServices] IHttpContextAccessor contextAccessor, 
        CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await usersFavouriteService.GetFavouritesAsync(userId, cancellationToken);
    }
    
    private static async Task<IResult> CheckIfPropertyIsFavourite([FromServices] IUsersFavouriteService usersFavouriteService,
        [FromServices] IHttpContextAccessor contextAccessor, 
        [FromRoute] Guid propertyId,
        CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await usersFavouriteService.GetIsInFavouritesForUserAsync(userId, propertyId, cancellationToken);
    }
}