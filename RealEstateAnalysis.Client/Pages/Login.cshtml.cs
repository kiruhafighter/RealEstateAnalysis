using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public UserLoginDto UserLoginDto { get; set; } = new ();

    public string ErrorMessage { get; set; } = string.Empty;

    private readonly IClient _client;

    public LoginModel(IClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid data";
            return Page();
        }

        try
        {
            var tokenDto = await _client.LoginUserAsync(UserLoginDto, default);
            
            Response.Cookies.Append("jwtToken", tokenDto.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenDto.AccessTokenExpiryTime
            });
            
            return RedirectToPage("/Index");
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
}