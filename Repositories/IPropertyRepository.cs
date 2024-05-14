using System.Linq.Expressions;
using Domain.Entities;
using Domain.SpecialData;

namespace Repositories;

public interface IPropertyRepository : IBaseRepository<Property, Guid>
{
    Task<IList<Property>> GetManyByConditionAsync(Expression<Func<Property, bool>> where, CancellationToken cancellationToken);

    Task<bool> UpdatePropertyAsync(Property property, CancellationToken cancellationToken);

    Task<(int, IList<Property>)> GetManyPagedAsync(int page, int pageSize, Expression<Func<Property, bool>> where,
        CancellationToken cancellationToken);
    
    Task<IList<AveragePriceForMonth>> GetAveragePriceForTimePeriodAsync(Expression<Func<Property, bool>> where,
        DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}