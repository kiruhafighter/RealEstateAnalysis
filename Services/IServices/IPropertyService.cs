using Microsoft.AspNetCore.Http;

namespace Services.IServices;

public interface IPropertyService
{
    Task<IResult> GetAgentPropertiesAsync(Guid agentId, CancellationToken cancellationToken);
        
    Task<IResult> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}