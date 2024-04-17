namespace Services.DTOs.PropertyDTOs;

public sealed record PropertyListedDto
{
    public Guid Id { get; init; }
        
    public string Name { get; init; } = default!;
        
    public string Country { get; init; } = default!;
        
    public string PropertyType { get; init; } = default!;
        
    public int? YearBuilt { get; init; }
        
    public int FloorArea { get; init; }
        
    public decimal Price { get; init; }
        
    public string PropertyStatus { get; init; } = default!;
        
    public string? FirstImage { get; init; }
}