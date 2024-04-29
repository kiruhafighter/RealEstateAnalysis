namespace Services.DTOs.UserDTOs;

public record UserDetailsDto
{
    public Guid Id { get; init; }
    
    public string Email { get; init; } = default!;
    
    public string FirstName { get; init; } = default!;
    
    public string LastName { get; init; } = default!;
}