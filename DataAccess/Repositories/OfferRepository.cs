using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class OfferRepository : BaseRepository<Offer, Guid>, IOfferRepository
{
    public OfferRepository(RealEstateDBContext context) : base(context)
    {
    }
    
    public override async Task<Offer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Offer>()
            .Include(o => o.Property)
                .ThenInclude(o => o!.Agent)
            .Include(o => o.OfferStatus)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> UpdateOfferAsync(Guid id, decimal offerAmount, string? comment, CancellationToken cancellationToken)
    {
        var updated = await Context.Set<Offer>()
            .Where(o => o.Id.Equals(id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.OfferAmount, offerAmount)
                .SetProperty(o => o.Comment, comment), cancellationToken);
        
        return updated > 0;
    }

    public async Task<IList<Offer>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Offer>()
            .Include(o => o.Property)
            .Include(o => o.OfferStatus)
            .Include(o => o.User)
            .Where(o => o.UserId.Equals(userId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Offer>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Offer>()
            .Include(o => o.Property)
            .Include(o => o.OfferStatus)
            .Include(o => o.User)
            .Where(o => o.PropertyId.Equals(propertyId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Offer>> GetByPropertyIdAndUserIdAsync(Guid propertyId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Offer>()
            .Include(o => o.Property)
            .Include(o => o.OfferStatus)
            .Include(o => o.User)
            .Where(o => o.PropertyId.Equals(propertyId) && o.UserId.Equals(userId))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateOfferStatusAsync(Guid offerId, int offerStatusId, CancellationToken cancellationToken)
    {
        var updated = await Context.Set<Offer>()
            .Where(o => o.Id.Equals(offerId))
            .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.OfferStatusId, offerStatusId), cancellationToken);
        
        return updated > 0;
    }
}