using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        // Clear the jwtToken cookie
        Response.Cookies.Delete("jwtToken");

        // Redirect to home page
        return RedirectToPage("/Index");
    }
}