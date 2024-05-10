using Microsoft.AspNetCore.Mvc;
using RealEstateAnalysis.Utils;
using Services.DTOs.AgentDTOs;
using Services.IServices;

namespace RealEstateAnalysis.Endpoints;

public static class AgentEndpoints
{
    public static WebApplication AddAgentEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGet($"/{RouteNameConstants.Agents}/{RouteNameConstants.MyProfile}",
                GetAgentProfileForUser)
            .RequireRole("Agent")
            .Produces<AgentDetailsDto>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(AgentEndpoints))
            .WithName(nameof(GetAgentProfileForUser))
            .WithOpenApi();
        
        webApplication.MapGet($"/{RouteNameConstants.Agents}/{{agentId}}",
                GetAgentAccountDetails)
            .AllowAnonymous()
            .Produces<AgentDetailsDto>()
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(nameof(AgentEndpoints))
            .WithName(nameof(GetAgentAccountDetails))
            .WithOpenApi();
        
        webApplication.MapPost($"/{RouteNameConstants.Agents}", AddAgentAccount)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(AgentEndpoints))
            .WithName(nameof(AddAgentAccount))
            .WithOpenApi();
        
        webApplication.MapPut($"/{RouteNameConstants.Agents}/{RouteNameConstants.MyProfile}",
            UpdateAgentInfoForUser)
            .RequireRole("Agent")
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<int>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(nameof(AgentEndpoints))
            .WithName(nameof(UpdateAgentInfoForUser))
            .WithOpenApi();
        
        return webApplication;
    }

    private static async Task<IResult> AddAgentAccount([FromServices] IAgentService agentService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] AddAgentDto agentDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await agentService.AddAgentAccountAsync(userId, agentDto, cancellationToken);
    }
    
    private static async Task<IResult> GetAgentAccountDetails([FromServices] IAgentService agentService,
        [FromRoute] Guid agentId, CancellationToken cancellationToken)
    {
        return await agentService.GetAgentAccountDetailsAsync(agentId, cancellationToken);
    }
    
    private static async Task<IResult> GetAgentProfileForUser([FromServices] IAgentService agentService,
        [FromServices] IHttpContextAccessor contextAccessor, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await agentService.GetAgentByUserIdAsync(userId, cancellationToken);
    }
    
    private static async Task<IResult> UpdateAgentInfoForUser([FromServices] IAgentService agentService,
        [FromServices] IHttpContextAccessor contextAccessor,
        [FromBody] UpdateAgentDto agentDto, CancellationToken cancellationToken)
    {
        if (!contextAccessor.TryGetUserId(out Guid userId))
        {
            return Results.Unauthorized();
        }
        
        return await agentService.UpdateAgentInfoAsync(userId, agentDto, cancellationToken);
    }
}