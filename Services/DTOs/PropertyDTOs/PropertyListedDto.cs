namespace Services.DTOs.PropertyDTOs
{
    public sealed record PropertyListedDto
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; } = default!;

        public string Address { get; init; } = default!;
    
        public string Description { get; init; } = default!;
    
        public string County { get; init; } = default!;
    
        public string Country { get; init; } = default!;
    
        public string Locality { get; init; } = default!;
    
        public string Postcode { get; init; } = default!;
        
        public string PropertyType { get; init; } = default!;
        
        public int NumberOfRooms { get; init; }
    
        public int? NumberOfFloors { get; init; }
    
        public int? YearBuilt { get; init; }
    
        public int? PlotArea { get; init; }
    
        public int FloorArea { get; init; }

        public decimal Price { get; init; }
        
        public string PropertyStatus { get; init; } = default!;

        public int NumberOfOffers { get; init; }
        
        public string? FirstImage { get; init; }
    }
}