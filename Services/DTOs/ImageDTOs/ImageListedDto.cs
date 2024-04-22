namespace Services.DTOs.ImageDTOs;

public sealed record ImageListedDto
{
    public int Id { get; init; }
    
    public Guid PropertyId { get; init; }
    
    public string ImagePath { get; init; } = default!;
}