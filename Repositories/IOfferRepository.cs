using Domain.Entities;

namespace Repositories;

public interface IOfferRepository : IBaseRepository<Offer, Guid>
{
    Task<bool> UpdateOfferAsync(Guid id, decimal offerAmount, string? comment, CancellationToken cancellationToken);
    
    Task<IList<Offer>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    
    Task<IList<Offer>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken = default);
    
    Task<IList<Offer>> GetByPropertyIdAndUserIdAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken = default);
    
    Task<bool> UpdateOfferStatusAsync(Guid offerId, int offerStatusId, CancellationToken cancellationToken);
}