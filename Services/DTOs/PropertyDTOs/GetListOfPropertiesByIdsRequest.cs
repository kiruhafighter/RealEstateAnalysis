namespace Services.DTOs.PropertyDTOs
{
    public sealed record GetListOfPropertiesByIdsRequest(IEnumerable<Guid> PropertyIds);
}