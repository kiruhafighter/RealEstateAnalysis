namespace Domain.Entities;

public class Image : Entity<int>
{
    public Guid PropertyId { get; set; }

    public string ImagePath { get; set; } = default!;
}