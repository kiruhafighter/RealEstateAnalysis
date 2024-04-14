namespace Domain.Entities;

public class PropertyStatus : Entity<int>
{
    public string StatusName { get; set; } = default!;
}