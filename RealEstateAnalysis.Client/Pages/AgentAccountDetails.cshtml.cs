using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstateAnalysis.Client.Pages;

public class AgentAccountDetailsModel : PageModel
{
    private readonly IClient _client;

    public AgentAccountDetailsModel(IClient client)
    {
        _client = client;
    }

    [BindProperty]
    public AgentDetailsDto? AgentDetails { get; set; }
    
    [BindProperty]
    public UpdateAgentDto UpdateAgentInfoDto { get; set; } = new();
    
    [BindProperty]
    public bool IsEditing { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        try
        {
            AgentDetails = await _client.GetAgentProfileForUserAsync();
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return RedirectToPage("/CreateAgentAccount");
        }
    }

    public async Task<IActionResult> OnPost(string? edit, string? cancel)
    {
        if (AgentDetails is null)
        {
            ErrorMessage = "Agent details not found.";
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
            await _client.UpdateAgentInfoForUserAsync(UpdateAgentInfoDto);

            IsEditing = false;

            AgentDetails = await _client.GetAgentProfileForUserAsync();

            return Page();
        }
        catch (ApiException<string> ex)
        {
            ErrorMessage = ex.Result;
            return Page();
        }
    }
}