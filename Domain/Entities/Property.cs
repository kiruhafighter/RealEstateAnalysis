namespace Domain.Entities;

public class Property : Entity<Guid>
{
    public string Name { get; set; } = default!;

    public string Address { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public string County { get; set; } = default!;
    
    public string Country { get; set; } = default!;
    
    public string Locality { get; set; } = default!;
    
    public string Postcode { get; set; } = default!;

    public int PropertyTypeId { get; set; }
    
    public PropertyType? PropertyType { get; set; }
    
    public int NumberOfRooms { get; set; }
    
    public int? NumberOfFloors { get; set; }
    
    public int? YearBuilt { get; set; }
    
    public int? PlotArea { get; set; }
    
    public int FloorArea { get; set; }

    public decimal Price { get; set; }
    
    public int PropertyStatusId { get; set; }
    
    public PropertyStatus? PropertyStatus { get; set; }
    
    public Guid AgentId { get; set; }
    
    public Agent? Agent { get; set; }
    
    public List<Image> Images { get; set; } = new();
    
    public List<Offer> Offers { get; set; } = new();
}