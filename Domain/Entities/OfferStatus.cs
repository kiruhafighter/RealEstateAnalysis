namespace Domain.Entities;

public class OfferStatus : Entity<int>
{
    public string StatusName { get; set; } = default!;
}