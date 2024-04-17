namespace Services.DTOs.AgentDTOs;

public sealed record UpdateAgentDto
{
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;
    
    public string? AgencyName { get; set; }
}