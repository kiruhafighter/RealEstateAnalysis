namespace Services.DTOs.PropertyDTOs;

public sealed record FilterPropertiesRequest(
    int PageNumber,
    int PageSize,
    string? Name,
    string? Address,
    string? County,
    string? Country,
    string? Locality,
    int? PropertyTypeId,
    int? NumberOfRooms,
    int? NumberOfFloors,
    int? MinYearBuilt,
    int? MaxYearBuilt,
    int? MinPlotArea,
    int? MaxPlotArea,
    int? MinFloorArea,
    int? MaxFloorArea,
    decimal? MinPrice,
    decimal? MaxPrice);