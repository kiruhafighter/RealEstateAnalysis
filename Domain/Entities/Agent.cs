namespace Domain.Entities;

public class Agent : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;
    
    public string? AgencyName { get; set; }
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
    
    public List<Property> Properties { get; set; } = new();
}