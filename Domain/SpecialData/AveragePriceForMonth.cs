namespace Domain.SpecialData;

public sealed record AveragePriceForMonth
{
    public int Year { get; init; }
        
    public int Month { get; init; }
        
    public decimal AveragePrice { get; init; }
}