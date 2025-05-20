using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class MyPropertiesModel : PageModel
{
    private readonly IClient _client;

    public MyPropertiesModel(IClient client)
    {
        _client = client;
    }

    public ICollection<PropertyListedDto> Properties { get; set; } = new List<PropertyListedDto>();
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var agentDetails = await _client.GetAgentProfileForUserAsync();
        if (agentDetails == null)
        {
            ErrorMessage = "Unable to find an agent account.";
            return Page();
        }
        try
        {
            Properties = await _client.GetAgentPropertiesAsync(agentDetails.Id);
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
        }
        return Page();
    }
}
