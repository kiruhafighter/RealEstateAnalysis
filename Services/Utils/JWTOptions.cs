namespace Services.Utils;

public sealed record JWTOptions
{
    public string Issuer { get; init; } = default!;

    public string Audience { get; init; } = default!;

    public string Key { get; init; } = default!;

    public int TokenExpiresInMinutes { get; init; }
}