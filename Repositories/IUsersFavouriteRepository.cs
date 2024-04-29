using Domain.Entities;

namespace Repositories;

public interface IUsersFavouriteRepository : IBaseRepository<UsersFavourite, int>
{
    Task<bool> ExistsForUserAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken = default);
    
    Task<bool> RemoveForUserAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken = default);
    
    Task<IList<UsersFavourite>> GetForUserAsync(Guid userId, CancellationToken cancellationToken = default);
}