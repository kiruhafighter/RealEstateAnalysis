namespace Services.DTOs.UserDTOs;

public sealed record UpdateUserInfoDto
{
    public string Email { get; init; } = default!;
    
    public string FirstName { get; init; } = default!;
    
    public string LastName { get; init; } = default!;
}