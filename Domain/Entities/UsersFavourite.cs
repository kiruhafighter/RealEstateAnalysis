namespace Domain.Entities;

public class UsersFavourite : Entity<int>
{
    public Guid PropertyId { get; set; }
    
    public Property? Property { get; set; }
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}