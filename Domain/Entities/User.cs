namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string Email { get; set; } = default!;
    
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public string PasswordHash { get; set; } = default!;
    
    public string PasswordSalt { get; set; } = default!;
    
    public int RoleId { get; set; }
    
    public Role? Role { get; set; }
    
    public List<UsersFavourite> UsersFavourites { get; set; } = new();
}