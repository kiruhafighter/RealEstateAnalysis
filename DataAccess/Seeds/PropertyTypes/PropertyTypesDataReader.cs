using Domain.Entities;

namespace DataAccess.Seeds.PropertyTypes;

internal static class PropertyTypesDataReader
{
    private const string ResourceName = "Seeds.PropertyTypes.PropertyTypes.json";
    
    internal static List<PropertyType> GetAll()
    {
        return DataReader<PropertyType>.Read(ResourceName);
    }
}