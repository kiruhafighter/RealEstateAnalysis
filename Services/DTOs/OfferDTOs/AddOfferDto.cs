namespace Services.DTOs.OfferDTOs
{
    public sealed record AddOfferDto
    {
        public Guid PropertyId { get; init; }
        
        public decimal OfferAmount { get; init; }
        
        public string Comment { get; init; } = default!;
    }
}