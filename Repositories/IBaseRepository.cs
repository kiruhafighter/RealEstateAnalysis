using Domain.Entities;

namespace Repositories
{
    public interface IBaseRepository<T, V>
    where T : Entity<V>
    where V : struct
    {
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(V id, CancellationToken cancellationToken = default);
        
        Task<bool> ExistsAsync(V id, CancellationToken cancellationToken = default);
        
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        
        Task<T?> GetByIdAsync(V id, CancellationToken cancellationToken = default);
    }
}