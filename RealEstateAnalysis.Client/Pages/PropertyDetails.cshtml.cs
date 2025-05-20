using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class PropertyDetailsModel : PageModel
{
    private readonly IClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PropertyDetailsModel(IClient client, IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public PropertyDetailsDto? Property { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public bool IsFavourited { get; set; }
    
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        try
        {
            Property = await _client.GetPropertyDetailsAsync(id);

            if (IsAuthorized())
            {
                IsFavourited = await _client.CheckIfPropertyIsFavouriteAsync(id);
            }
            
            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }
    }
    
    public async Task<IActionResult> OnPostAddToFavouritesAsync(Guid id)
    {
        try
        {
            if (IsAuthorized())
            {
                await _client.AddUsersFavouriteAsync(id);
            }
            else
            {
                ErrorMessage = "You need to be logged in to add properties to your favourites.";
            }
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }

        return RedirectToPage("/PropertyDetails", new { id });
    }
    
    public async Task<IActionResult> OnPostRemoveFromFavouritesAsync(Guid id)
    {
        try
        {
            if (IsAuthorized())
            {
                await _client.RemovePropertyFromFavouritesAsync(id);
            }
            else
            {
                ErrorMessage = "You need to be logged in to remove properties from your favourites.";
            }
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }

        return RedirectToPage("/PropertyDetails", new { id });
    }

    private string? GetUserFromToken()
    {
        var token = GetTokenFromCookie();
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        
        return userIdClaim?.Value;
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
    
    public bool IsPropertyAgent()
    {
        var userId = GetUserFromToken();
        return userId != null && Property?.Agent?.UserId.ToString() == userId; 
    }

    public bool IsAuthorized()
    {
        var userId = GetUserFromToken();

        return userId is not null;
    }
}