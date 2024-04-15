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
                .FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
        }
    }
}