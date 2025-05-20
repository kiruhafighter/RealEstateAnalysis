namespace Services.DTOs.UsersFavouriteDTOs
{
    public sealed record UsersFavouriteListedDto
    {
        public int Id { get; init; }

        public string PropertyName { get; init; } = default!;
        
        public string? FirstImage { get; init; }
        
        public decimal Price { get; init; }
        
        public Guid PropertyId { get; init; }
    }
}