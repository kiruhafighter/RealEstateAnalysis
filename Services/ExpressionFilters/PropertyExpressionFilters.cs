using System.Linq.Expressions;
using Domain.Entities;

namespace Services.ExpressionFilters
{
    internal static class PropertyExpressionFilters
    {
        internal static Expression<Func<Property, bool>> FilterByAgentId(Guid agentId)
        {
            return pr =>
                pr.AgentId.Equals(agentId);
        }
    }
}