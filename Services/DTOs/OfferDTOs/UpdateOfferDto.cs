namespace Services.DTOs.OfferDTOs
{
    public sealed record UpdateOfferDto
    {
        public Guid Id { get; init; }

        public decimal OfferAmount { get; init; }
        
        public string? Comment { get; init; }
    }
}