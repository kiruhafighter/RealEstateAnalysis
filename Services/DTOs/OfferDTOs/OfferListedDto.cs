namespace Services.DTOs.OfferDTOs
{
    public sealed record OfferListedDto
    {
        public Guid Id { get; init; }
        
        public Guid PropertyId { get; init; }
        
        public string PropertyName { get; init; } = default!;
    
        public decimal OfferAmount { get; init; }
        
        public DateTime OfferDate { get; init; }
        
        public int OfferStatusId { get; init; }
        
        public string OfferStatusName { get; init; } = default!;
        
        public string UserEmail { get; init; } = default!;
    }
}