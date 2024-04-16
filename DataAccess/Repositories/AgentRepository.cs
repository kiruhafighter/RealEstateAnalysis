using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories
{
    internal sealed class AgentRepository : BaseRepository<Agent, Guid>, IAgentRepository
    {
        public AgentRepository(DBContext context) : base(context)
        {
        }
        
        public async Task<Agent?> GetAgentByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await Context.Set<Agent>()
                .AsNoTracking()
                .Include(a => a.Properties)
                    .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(a => a.UserId.Equals(userId), 
                    cancellationToken);
        }
        
        public async Task<bool> AgentExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var agent = await Context.Set<Agent>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserId.Equals(userId), 
                    cancellationToken);
            
            return agent is not null;
        }

        public async Task<bool> AgentExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var agent = await Context.Set<Agent>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email.Equals(email), 
                    cancellationToken);
            
            return agent is not null;
        }

        public async Task<bool> UpdateAgentAsync(Agent agent, CancellationToken cancellationToken)
        {
            var updated = await Context.Set<Agent>()
                .Where(a => a.Id.Equals(agent.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.Email, agent.Email)
                    .SetProperty(a => a.FirstName, agent.FirstName)
                    .SetProperty(a => a.LastName, agent.LastName)
                    .SetProperty(a => a.PhoneNumber, agent.PhoneNumber)
                    .SetProperty(a => a.AgencyName, agent.AgencyName), cancellationToken);

            return updated > 0;
        }
    }
}