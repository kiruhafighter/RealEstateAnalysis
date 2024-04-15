using Domain.Entities;
using Repositories;

namespace DataAccess.Repositories
{
    internal sealed class AgentRepository : BaseRepository<Agent, Guid>, IAgentRepository
    {
        public AgentRepository(DBContext context) : base(context)
        {
        }
    }
}