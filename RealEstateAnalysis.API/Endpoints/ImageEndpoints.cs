using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.ImageDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints;

internal static class ImageEndpoints
{
    public static WebApplication AddImageEndpoints(this WebApplication webApplication)
    {
        webApplication.MapPost($"/{RouteNameConstants.Properties}/{{propertyId}}/{RouteNameConstants.Images}",
                AddImageForProperty)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(ImageEndpoints))
            .WithName(nameof(AddImageForProperty))
            .WithOpenApi();
        
        webApplication.MapDelete($"/{RouteNameConstants.Properties}/{{propertyId}}/{RouteNameConstants.Images}/{{imageId}}",
                DeleteImageForProperty)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(ImageEndpoints))
            .WithName(nameof(DeleteImageForProperty))
            .WithOpenApi();
        
        return webApplication;
    }
    
    private static async Task<IResult> AddImageForProperty([FromServices] IImageService imageService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid propertyId,
        [FromBody] AddImageDto imageAddInfo, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await imageService.AddImageForProperty(userId, propertyId, imageAddInfo, cancellationToken);
    }
    
    private static async Task<IResult> DeleteImageForProperty([FromServices] IImageService imageService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid propertyId,
        [FromRoute] int imageId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await imageService.DeleteImageForProperty(userId, propertyId, imageId, cancellationToken);
    }
}