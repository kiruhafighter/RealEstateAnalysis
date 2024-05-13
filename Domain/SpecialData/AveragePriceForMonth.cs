namespace Domain.SpecialData;

public sealed record AveragePriceForMonth
{
    private int Year { get; init; }
        
    public int Month { get; init; }
        
    public decimal AveragePrice { get; init; }
}