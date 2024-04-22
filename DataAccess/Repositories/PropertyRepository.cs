using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class PropertyRepository : BaseRepository<Property, Guid>, IPropertyRepository
{
    public PropertyRepository(DbContext context) : base(context)
    {
    }
    
    public async Task<IList<Property>> GetManyByConditionAsync(Expression<Func<Property, bool>> where, CancellationToken cancellationToken)
    {
        return await Context.Set<Property>()
            .Where(where)
            .Include(p => p.PropertyStatus)
            .Include(p => p.PropertyType)
            .Include(p => p.Images)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdatePropertyAsync(Property property, CancellationToken cancellationToken)
    {
        var entry = Context.Entry(property);

        entry.State = EntityState.Modified;
        entry.Property(p => p.AgentId).IsModified = false;

        var update = await Context.SaveChangesAsync(cancellationToken);
        return update > 0;
    }

    public override async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var property = await Context.Set<Property>()
            .Include(p => p.PropertyType)
            .Include(p => p.PropertyStatus)
            .Include(p => p.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
        
        return property;
    }
}