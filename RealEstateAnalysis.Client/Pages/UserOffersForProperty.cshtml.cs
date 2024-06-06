using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateAnalysis.Client.Enums;

namespace RealEstateAnalysis.Client.Pages;

public class UserOffersForPropertyModel : PageModel
{
    private readonly IClient _client;

    public UserOffersForPropertyModel(IClient client)
    {
        _client = client;
    }
    
    public ICollection<OfferListedDto> Offers { get; set; } = new List<OfferListedDto>();
    
    public string? ErrorMessage { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public Guid PropertyId { get; set; }
    
    public async Task<IActionResult> OnGetAsync(Guid propertyId)
    {
        try
        {
            Offers = await _client.GetUserOffersForPropertyAsync(propertyId);
            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(Guid offerId, Guid propertyId)
    {
        try
        {
            await _client.DeleteOfferAsync(offerId);
            return RedirectToPage(new { propertyId });
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return await OnGetAsync(propertyId);
        }
    }
    
    public bool CanDeleteOffer(OfferListedDto offer)
    {
        return offer.OfferStatusId == (int)OfferStatus.Pending;
    }
}