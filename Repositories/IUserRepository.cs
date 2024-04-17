using Domain.Entities;

namespace Repositories;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> UpdateUserInfoAsync(Guid id, string email, string firstName, string lastName,
        CancellationToken cancellationToken);

    Task<bool> UpdateUserRoleAsync(Guid id, int newRoleId, CancellationToken cancellationToken);
}