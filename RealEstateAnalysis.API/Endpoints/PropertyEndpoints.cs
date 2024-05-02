using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.PropertyDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints;

public static class PropertyEndpoints
{
    public static WebApplication AddPropertyEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGet($"/{RouteNameConstants.Properties}/{{propertyId}}",
                GetPropertyDetails)
            .AllowAnonymous()
            .Produces<PropertyDetailsDto>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(nameof(PropertyEndpoints))
            .WithName(nameof(GetPropertyDetails))
            .WithOpenApi();
        
        webApplication.MapGet($"/{RouteNameConstants.Agents}/{{agentId}}/{RouteNameConstants.Properties}",
                GetAgentProperties)
            .AllowAnonymous()
            .Produces<List<PropertyListedDto>>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(nameof(PropertyEndpoints))
            .WithName(nameof(GetAgentProperties))
            .WithOpenApi();
        
        webApplication.MapPost($"/{RouteNameConstants.Properties}", AddProperty)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(PropertyEndpoints))
            .WithName(nameof(AddProperty))
            .WithOpenApi();
        
        webApplication.MapPut($"/{RouteNameConstants.Properties}", UpdateProperty)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(PropertyEndpoints))
            .WithName(nameof(UpdateProperty))
            .WithOpenApi();

        return webApplication;
    }
    
    private static async Task<IResult> GetPropertyDetails([FromServices] IPropertyService propertyService,
        [FromRoute] Guid propertyId, CancellationToken cancellationToken)
    {
        return await propertyService.GetByIdAsync(propertyId, cancellationToken);
    }
    
    private static async Task<IResult> GetAgentProperties([FromServices] IPropertyService propertyService,
        [FromRoute] Guid agentId, CancellationToken cancellationToken)
    {
        return await propertyService.GetAgentPropertiesAsync(agentId, cancellationToken);
    }
    
    private static async Task<IResult> AddProperty([FromServices] IPropertyService propertyService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] AddPropertyDto addPropertyDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await propertyService.AddPropertyAsync(userId, addPropertyDto, cancellationToken);
    }
    
    private static async Task<IResult> UpdateProperty([FromServices] IPropertyService propertyService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] UpdatePropertyDto updatePropertyDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await propertyService.UpdateAsync(userId, updatePropertyDto, cancellationToken);
    }
}