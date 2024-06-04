using RealEstateAnalysis.Client.Enums;

namespace RealEstateAnalysis.Client.ViewModels;

public class GetFilteredPropertiesRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? County { get; set; }
    public string? Country { get; set; }
    public string? Locality { get; set; }
    public PropertyType? PropertyTypeId { get; set; }
    public int? NumberOfRooms { get; set; }
    public int? NumberOfFloors { get; set; }
    public int? MinYearBuilt { get; set; }
    public int? MaxYearBuilt { get; set; }
    public int? MinPlotArea { get; set; }
    public int? MaxPlotArea { get; set; }
    public int? MinFloorArea { get; set; }
    public int? MaxFloorArea { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}