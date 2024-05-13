using System.Linq.Expressions;
using Domain.Entities;
using Domain.SpecialData;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class PropertyRepository : BaseRepository<Property, Guid>, IPropertyRepository
{
    public PropertyRepository(RealEstateDBContext context) : base(context)
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

    public async Task<(int, IList<Property>)> GetManyPagedAsync(int page, int pageSize, Expression<Func<Property, bool>> where, CancellationToken cancellationToken)
    {
        int totalCount = await Context.Set<Property>()
            .Where(where)
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var properties = await Context.Set<Property>()
            .Where(where)
            .Include(p => p.Agent)
                .ThenInclude(a => a!.User)
            .Include(p => p.PropertyType)
            .Include(p => p.PropertyStatus)
            .Include(p => p.Images)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (totalCount, properties);
    }

    public async Task<IList<AveragePriceForMonth>> GetAveragePriceForTimePeriodAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var averagePricesForMonths = await Context.Set<Property>()
            .TemporalBetween(startDate, endDate)
            .Select(p => new
            {
                p.Id,
                p.Price,
                ValidFrom = EF.Property<DateTime>(p, "ValidFrom"),
                ValidTo = EF.Property<DateTime>(p, "ValidTo")
            }) 
            .GroupBy(p => new
            {
                p.Id,
                p.ValidFrom.Month,
                p.ValidFrom.Year
            })
            .ToListAsync(cancellationToken);

        throw new NotImplementedException();
    }

    public override async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var property = await Context.Set<Property>()
            .Include(p => p.Agent)
                .ThenInclude(a => a!.User)
            .Include(p => p.PropertyType)
            .Include(p => p.PropertyStatus)
            .Include(p => p.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
        
        return property;
    }
}