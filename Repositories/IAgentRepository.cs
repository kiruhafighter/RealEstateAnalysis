using Domain.Entities;

namespace Repositories;

public interface IAgentRepository : IBaseRepository<Agent, Guid>
{
    Task<Agent?> GetAgentByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> AgentExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> AgentExistsByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> UpdateAgentAsync(Agent agent, CancellationToken cancellationToken);
}