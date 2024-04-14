namespace Domain.Entities;

public class PropertyType : Entity<int>
{
    public string TypeName { get; set; } = default!;
}