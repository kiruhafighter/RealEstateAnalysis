using Microsoft.AspNetCore.Http;
using Services.DTOs;
using Services.DTOs.PropertyDTOs;

namespace Services.IServices;

public interface IPropertyService
{
    Task<IResult> GetPropertiesFilteredAsync(FilterPropertiesRequest filterRequest, CancellationToken cancellationToken);
    
    Task<IResult> GetListOfPropertiesByIdsAsync(GetListOfPropertiesByIdsRequest getListOfPropertiesByIdsRequest, CancellationToken cancellationToken);
    
    Task<IResult> GetAgentPropertiesAsync(Guid agentId, CancellationToken cancellationToken);
        
    Task<IResult> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IResult> AddPropertyAsync(Guid userId, AddPropertyDto addPropertyInfo, CancellationToken cancellationToken);

    Task<IResult> UpdateAsync(Guid userId, UpdatePropertyDto updatePropertyInfo, CancellationToken cancellationToken);

    Task<IResult> GetAveragePricesForTimePeriodAsync(GetAveragePricesSampleRequest request, CancellationToken cancellationToken);
}