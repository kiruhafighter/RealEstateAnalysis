namespace Services.DTOs.AgentDTOs;

public sealed record AgentDetailsDto
{
    public Guid Id { get; init; }
        
    public string FirstName { get; init; } = default!;
    
    public string LastName { get; init; } = default!;
    
    public string Email { get; init; } = default!;
    
    public string PhoneNumber { get; init; } = default!;
    
    public string? AgencyName { get; init; }
    
    public Guid UserId { get; init; }
}