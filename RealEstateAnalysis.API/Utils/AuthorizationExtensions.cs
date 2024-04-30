using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateAnalysis.Utils;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddApiAuthorizationForUser(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.NameIdentifier)
                .Build());
            
        return services;
    }
}