using Domain.Entities;

namespace DataAccess.Seeds.PropertyStatuses;

internal static class PropertyStatusesDataReader
{
    private const string ResourceName = "Seeds.PropertyStatuses.PropertyStatuses.json";
    
    internal static List<PropertyStatus> GetAll()
    {
        return DataReader<PropertyStatus>.Read(ResourceName);
    }
}