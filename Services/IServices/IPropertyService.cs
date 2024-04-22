using Microsoft.AspNetCore.Http;
using Services.DTOs.PropertyDTOs;

namespace Services.IServices;

public interface IPropertyService
{
    Task<IResult> GetAgentPropertiesAsync(Guid agentId, CancellationToken cancellationToken);
        
    Task<IResult> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IResult> AddAsync(Guid userId, AddPropertyDto addPropertyInfo, CancellationToken cancellationToken);

    Task<IResult> UpdateAsync(Guid userId, UpdatePropertyDto updatePropertyInfo, CancellationToken cancellationToken);
}