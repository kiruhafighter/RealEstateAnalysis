namespace Services.DTOs.UserDTOs
{
    public sealed record AddUserDto
    {
        public string Email { get; init; } = default!;
        
        public string FirstName { get; init; } = default!;
        
        public string LastName { get; init; } = default!;

        public string Password { get; init; } = default!;

        public string ConfirmPassword { get; init; } = default!;
    }
}