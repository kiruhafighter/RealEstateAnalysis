namespace Services.DTOs.ImageDTOs;

public sealed record ImageListedDto
{
    public Guid Id { get; init; }
    
    public string ImagePath { get; init; } = default!;
}