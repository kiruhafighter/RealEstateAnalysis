using Services.DTOs.ImageDTOs;

namespace Services.DTOs.PropertyDTOs;

public sealed record AddPropertyDto
{
    public string Name { get; init; } = default!;

    public string Address { get; init; } = default!;
    
    public string Description { get; init; } = default!;
    
    public string County { get; init; } = default!;
    
    public string Country { get; init; } = default!;
    
    public string Locality { get; init; } = default!;
    
    public string Postcode { get; init; } = default!;

    public int PropertyTypeId { get; init; }
    
    public int NumberOfRooms { get; init; }
    
    public int? NumberOfFloors { get; init; }
    
    public int? YearBuilt { get; init; }
    
    public int? PlotArea { get; init; }
    
    public int FloorArea { get; init; }

    public decimal Price { get; init; }
    
    public int PropertyStatusId { get; init; }
    
    public List<AddImageDto> Images { get; init; } = [];
}