using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.OfferDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints;

internal static class OfferEndpoints
{
    public static WebApplication AddOfferEndpoints(this WebApplication webApplication)
    {
        webApplication.MapPost($"/{RouteNameConstants.Offers}",
                AddOffer)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(AddOffer))
            .WithOpenApi();
        
        webApplication.MapGet($"/{RouteNameConstants.Offers}/{{offerId}}",
                GetOfferDetails)
            .RequireAuthorization()
            .Produces<OfferDetailsDto>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(GetOfferDetails))
            .WithOpenApi();
        
        webApplication.MapGet($"/{RouteNameConstants.Offers}/{RouteNameConstants.MyOffers}",
                GetOffersForUser)
            .RequireAuthorization()
            .Produces<List<OfferListedDto>>()
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(GetOffersForUser))
            .WithOpenApi();
            
        webApplication.MapGet($"/{RouteNameConstants.Properties}/{{propertyId}}/{RouteNameConstants.Offers}",
                GetOffersByPropertyId)
            .RequireRole("Agent")
            .Produces<List<OfferListedDto>>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(GetOffersByPropertyId))
            .WithOpenApi();
            
        webApplication.MapGet($"/{RouteNameConstants.Properties}/{{propertyId}}/{RouteNameConstants.Offers}/{RouteNameConstants.MyOffers}",
                GetUserOffersForProperty)
            .RequireAuthorization()
            .Produces<List<OfferListedDto>>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(GetUserOffersForProperty))
            .WithOpenApi();
        
        webApplication.MapPut($"/{RouteNameConstants.Offers}", UpdateOffer)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(UpdateOffer))
            .WithOpenApi();
        
        webApplication.MapDelete($"/{RouteNameConstants.Offers}/{{offerId}}", DeleteOffer)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(DeleteOffer))
            .WithOpenApi();
        
        webApplication.MapPatch($"/{RouteNameConstants.Offers}/{{offerId}}/{RouteNameConstants.Accept}",
                AcceptOffer)
            .RequireRole("Agent")
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(AcceptOffer))
            .WithOpenApi();
        
        webApplication.MapPatch($"/{RouteNameConstants.Offers}/{{offerId}}/{RouteNameConstants.Reject}",
                RejectOffer)
            .RequireRole("Agent")
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(OfferEndpoints))
            .WithName(nameof(RejectOffer))
            .WithOpenApi();

        return webApplication;
    }
    
    private static async Task<IResult> AddOffer([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] AddOfferDto offerDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.AddOfferAsync(userId, offerDto, cancellationToken);
    }
    
    private static async Task<IResult> GetOfferDetails([FromServices] IOfferService offerService,
        [FromRoute] Guid offerId, CancellationToken cancellationToken)
    {
        return await offerService.GetOfferByIdAsync(offerId, cancellationToken);
    }
    
    private static async Task<IResult> GetOffersForUser([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.GetOffersByUserIdAsync(userId, cancellationToken);
    }
    
    private static async Task<IResult> GetOffersByPropertyId([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid propertyId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.GetOffersByPropertyIdAsync(userId, propertyId, cancellationToken);
    }
    
    private static async Task<IResult> GetUserOffersForProperty([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid propertyId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.GetUserOffersForPropertyAsync(userId, propertyId, cancellationToken);
    }
    
    private static async Task<IResult> UpdateOffer([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] UpdateOfferDto offerDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.UpdateOfferAsync(userId, offerDto, cancellationToken);
    }
    
    private static async Task<IResult> DeleteOffer([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid offerId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.DeleteOfferAsync(userId, offerId, cancellationToken);
    }
    
    private static async Task<IResult> AcceptOffer([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid offerId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.ApproveOfferAsync(userId, offerId, cancellationToken);
    }
    
    private static async Task<IResult> RejectOffer([FromServices] IOfferService offerService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromRoute] Guid offerId, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await offerService.RejectOfferAsync(userId, offerId, cancellationToken);
    }
}