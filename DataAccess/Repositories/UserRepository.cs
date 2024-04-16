using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories
{
    internal sealed class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await Context.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);

            return user is not null;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await Context.Set<User>()
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<bool> UpdateUserInfoAsync(Guid id, string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            var updated = await Context.Set<User>()
                .Where(u => u.Id.Equals(id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Email, email)
                    .SetProperty(u => u.FirstName, firstName)
                    .SetProperty(u => u.LastName, lastName), cancellationToken);
            
            return updated > 0;
        }

        public async Task<bool> UpdateUserRoleAsync(Guid id, int newRoleId, CancellationToken cancellationToken)
        {
            var updated = await Context.Set<User>()
                .Where(u => u.Id.Equals(id))
                .ExecuteUpdateAsync(s => s
                        .SetProperty(u => u.RoleId, newRoleId),
                    cancellationToken);

            return updated > 0;
        }
    }
}