namespace Services.DTOs;

public sealed record CollectionResult<T> where T : class
{
    public ICollection<T> Data { get; set; } = [];
    
    public int TotalCount { get; set; }
}