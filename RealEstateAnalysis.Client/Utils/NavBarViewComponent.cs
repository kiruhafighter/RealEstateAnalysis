using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAnalysis.Client.Utils;

public sealed class NavBarViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NavBarViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IViewComponentResult Invoke()
    {
        var isAuthenticated = IsUserAuthenticated();
        return View(isAuthenticated);
    }

    private bool IsUserAuthenticated()
    {
        var token = GetTokenFromCookie();
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userRoleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

        return userRoleClaim != null;
    }

    private string? GetTokenFromCookie()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null && httpContext.Request.Cookies.ContainsKey("jwtToken"))
        {
            return httpContext.Request.Cookies["jwtToken"];
        }

        return null;
    }
}