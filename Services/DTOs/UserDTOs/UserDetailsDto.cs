namespace Services.DTOs.UserDTOs
{
    public record UserDetailsDto
    {
        public string Email { get; init; } = default!;
    
        public string FirstName { get; init; } = default!;
    
        public string LastName { get; init; } = default!;
    }
}