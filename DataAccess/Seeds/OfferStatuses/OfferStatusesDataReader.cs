using Domain.Entities;

namespace DataAccess.Seeds.OfferStatuses;

internal static class OfferStatusesDataReader
{
    private const string ResourceName = "Seeds.OfferStatuses.OfferStatuses.json";
    
    internal static List<OfferStatus> GetAll()
    {
        return DataReader<OfferStatus>.Read(ResourceName);
    }
}