using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository<T, V> : IBaseRepository<T, V>
    where T : Entity<V>
    where V : struct
    {
        protected readonly DbContext Context;
        
        public BaseRepository(DbContext context)
        {
            Context = context;
        }
        
        public virtual async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Add(entity);

            var added = await Context.SaveChangesAsync(cancellationToken);

            return added > 0;
        }
        
        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().AddRange(entities);

            var added = await Context.SaveChangesAsync(cancellationToken);

            return added > 0;
        }

        public virtual async Task<bool> DeleteAsync(V id, CancellationToken cancellationToken = default)
        {
            int deleted = await Context.Set<T>()
                .Where(t => t.Id.Equals(id))
                .ExecuteDeleteAsync(cancellationToken);
            
            return deleted > 0;
        }

        public virtual async Task<bool> ExistsAsync(V id, CancellationToken cancellationToken = default)
        {
            var entity = await Context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
            
            return entity is not null;
        }

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(V id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
        }
    }
}