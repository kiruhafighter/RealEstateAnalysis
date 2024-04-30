using System.Security.Claims;

namespace RealEstateAnalysis.Utils;

public static class HttpContextAccessorExtensions
{
    public static bool TryGetUserId(this IHttpContextAccessor contextAccessor, out Guid userId)
    {
        var userIdClaim = contextAccessor.HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) ||
            !Guid.TryParse(userIdClaim, out var userIdValue))
        {
            userId = Guid.Empty;
            return false;
        }

        userId = userIdValue;
        return true;
    }
}