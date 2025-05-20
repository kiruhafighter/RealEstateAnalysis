using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class UsersFavouriteRepository : BaseRepository<UsersFavourite, int>, IUsersFavouriteRepository
{
    public UsersFavouriteRepository(RealEstateDBContext context) : base(context)
    {
    }

    public async Task<bool> ExistsForUserAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<UsersFavourite>()
            .AnyAsync(uf => uf.UserId.Equals(userId) && uf.PropertyId.Equals(propertyId), cancellationToken);
    }

    public async Task<bool> RemoveForUserAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken = default)
    {
        var deleted = await Context.Set<UsersFavourite>()
            .Where(uf => uf.UserId.Equals(userId) && uf.PropertyId.Equals(propertyId))
            .ExecuteDeleteAsync(cancellationToken);
        
        return deleted > 0;
    }

    public async Task<IList<UsersFavourite>> GetForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<UsersFavourite>()
            .Where(uf => uf.UserId.Equals(userId))
            .Include(uf => uf.Property)
                .ThenInclude(p => p!.Images)
            .ToListAsync(cancellationToken);
    }
}