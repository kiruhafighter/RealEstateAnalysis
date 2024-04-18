namespace Services.DTOs.ImageDTOs;

public sealed record AddImageDto
{
    public string ImagePath { get; init; } = default!;        
}