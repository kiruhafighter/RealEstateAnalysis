using Domain.Entities;

namespace DataAccess.Seeds.Roles;

internal static class RolesDataReader
{
    private const string ResourceName = "Seeds.Roles.Roles.json";
    
    internal static List<Role> GetAll()
    {
        return DataReader<Role>.Read(ResourceName);
    }
}