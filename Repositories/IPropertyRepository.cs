using System.Linq.Expressions;
using Domain.Entities;

namespace Repositories;

public interface IPropertyRepository : IBaseRepository<Property, Guid>
{
    Task<IList<Property>> GetManyByConditionAsync(Expression<Func<Property, bool>> where, CancellationToken cancellationToken);
}