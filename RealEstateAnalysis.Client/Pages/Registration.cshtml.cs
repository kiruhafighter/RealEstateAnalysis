using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class RegistrationModel : PageModel
{
    [BindProperty]
    public AddUserDto AddUserDto { get; set; } = new AddUserDto();
    
    public string? ErrorMessage { get; set; }
    
    private readonly IClient _client;

    public RegistrationModel(IClient client)
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
            await _client.RegisterUserAsync(AddUserDto, default);
            return RedirectToPage("/Login");
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
}