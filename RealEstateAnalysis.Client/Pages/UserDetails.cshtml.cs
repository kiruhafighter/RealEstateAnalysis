using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class UserDetailsModel : PageModel
{
    private readonly IClient _client;

    public UserDetailsModel(IClient client)
    {
        _client = client;
    }

    [BindProperty]
    public UserDetailsDto? UserDetails { get; set; } = new ();
    
    [BindProperty]
    public UpdateUserInfoDto UpdateUserInfoDto { get; set; } = new ();
    
    [BindProperty]
    public bool IsEditing { get; set; }
    
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            UserDetails = await _client.GetUserDetailsAsync();
            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Login");
        }
    }

    public async Task<IActionResult> OnPostAsync(string? edit, string? cancel)
    {
        if (UserDetails is null)
        {
            ErrorMessage = "User details not found.";
            return Page();
        }
        
        if (!string.IsNullOrEmpty(edit))
        {
            IsEditing = true;
            return await OnGet();
        }
        
        if (!string.IsNullOrEmpty(cancel))
        {
            IsEditing = false;
            return RedirectToPage();
        }
        
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid form submission.";
            return Page();
        }

        try
        {
            await _client.UpdateUserDetailsAsync(UpdateUserInfoDto);

            IsEditing = false;

            UserDetails = await _client.GetUserDetailsAsync();

            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
}