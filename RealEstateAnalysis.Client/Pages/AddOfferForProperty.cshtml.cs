using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class AddOfferForPropertyModel : PageModel
{
    private readonly IClient _client;

    public AddOfferForPropertyModel(IClient client)
    {
        _client = client;
    }
    
    [BindProperty]
    public AddOfferDto AddOfferDto { get; set; } = new();
    
    public string? ErrorMessage { get; set; }

    public IActionResult  OnGet(Guid propertyId)
    {
        AddOfferDto.PropertyId = propertyId;
        return Page();
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
            await _client.AddOfferAsync(AddOfferDto);
            return RedirectToPage("/PropertyDetails", new { id = AddOfferDto.PropertyId });
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
    
    public IActionResult OnPostCancel()
    {
        return RedirectToPage("/PropertyDetails", new { id = AddOfferDto.PropertyId });
    }
}