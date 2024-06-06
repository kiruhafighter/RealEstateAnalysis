using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateAnalysis.Client.Enums;

namespace RealEstateAnalysis.Client.Pages;

public class OffersForPropertyModel : PageModel
{
    private readonly IClient _client;

    public OffersForPropertyModel(IClient client)
    {
        _client = client;
    }
    
    public ICollection<OfferListedDto> Offers { get; set; } = new List<OfferListedDto>();
    
    public string? ErrorMessage { get; set; }
    
    public Guid PropertyId { get; set; }
    
    public async Task<IActionResult> OnGetAsync(Guid propertyId)
    {
        PropertyId = propertyId;
        try
        {
            Offers = await _client.GetOffersByPropertyIdAsync(propertyId);
            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return RedirectToPage("/Error");
        }
    }
    
    public async Task<IActionResult> OnPostAsync(string accept, string reject, Guid offerId, Guid propertyId)
    {
        PropertyId = propertyId;
        try
        {
            if (!string.IsNullOrEmpty(accept))
            {
                await _client.AcceptOfferAsync(offerId);
            }
            else if (!string.IsNullOrEmpty(reject))
            {
                await _client.RejectOfferAsync(offerId);
            }

            return RedirectToPage(new { propertyId });
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
    
    public bool IsPendingOffer(OfferListedDto offer)
    {
        return offer.OfferStatusId == (int)OfferStatus.Pending;
    }
}