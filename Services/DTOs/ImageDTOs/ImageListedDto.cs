namespace Services.DTOs.ImageDTOs;

public sealed record ImageListedDto
{
    public Guid PropertyId { get; init; }
    
    public string ImagePath { get; init; } = default!;
}