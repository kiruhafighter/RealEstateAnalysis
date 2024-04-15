using Domain.Entities;

namespace Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}