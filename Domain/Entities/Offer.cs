namespace Domain.Entities;

public class Offer : Entity<Guid>
{
    public Guid PropertyId { get; set; }
    
    public Property? Property { get; set; }
    
    public decimal OfferAmount { get; set; }
    
    public DateTime OfferDate { get; set; }
    
    public int OfferStatusId { get; set; }
    
    public OfferStatus? OfferStatus { get; set; }
    
    public string? Comment { get; set; }
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}