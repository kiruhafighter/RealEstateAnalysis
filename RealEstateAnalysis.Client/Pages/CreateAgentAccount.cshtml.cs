using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class CreateAgentAccountModel : PageModel
{
    private readonly IClient _client;

    public CreateAgentAccountModel(IClient client)
    {
        _client = client;
    }

    [BindProperty]
    public AddAgentDto AddAgentDto { get; set; } = new();
    
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid data";
            return Page();
        }

        try
        {
            await _client.AddAgentAccountAsync(AddAgentDto);
            return RedirectToPage("/Index");
        }
        catch(ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
}