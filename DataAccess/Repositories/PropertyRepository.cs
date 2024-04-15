using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories
{
    internal sealed class PropertyRepository : BaseRepository<Property, Guid>, IPropertyRepository
    {
        public PropertyRepository(DbContext context) : base(context)
        {
        }
    }
}