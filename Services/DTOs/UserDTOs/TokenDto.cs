namespace Services.DTOs.UserDTOs;

public sealed record TokenDto
{
    public string AccessToken { get; init; } = default!;

    public DateTime AccessTokenExpiryTime { get; init; }
}