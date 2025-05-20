using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class FavouritesModel : PageModel
{
    private readonly IClient _client;

    public FavouritesModel(IClient client)
    {
        _client = client;
    }

    public ICollection<UsersFavouriteListedDto> Favourites { get; set; } = new List<UsersFavouriteListedDto>();
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Favourites = await _client.GetUsersFavouritesAsync();
        }
        catch (ApiException ex)
        {
            ErrorMessage = $"Error fetching favourites: {ex.Message}";
        }

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveAsync(int id)
    {
        try
        {
            await _client.RemoveUsersFavouriteAsync(id);
            Favourites = await _client.GetUsersFavouritesAsync();
        }
        catch (ApiException ex)
        {
            ErrorMessage = $"Error removing favourite: {ex.Message}";
        }

        return Page();
    }
}
