using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs.PropertyDTOs;
using Services.ExpressionFilters;
using Services.IServices;

namespace Services.Implementations;

public class PropertyService : IPropertyService
{
    private readonly IMapper _mapper;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAgentRepository _agentRepository;

    public PropertyService(IMapper mapper, IPropertyRepository propertyRepository, IAgentRepository agentRepository)
    {
        _mapper = mapper;
        _propertyRepository = propertyRepository;
        _agentRepository = agentRepository;
    }
        
    public async Task<IResult> GetAgentPropertiesAsync(Guid agentId, CancellationToken cancellationToken)
    {
        if (!await _agentRepository.ExistsAsync(agentId, cancellationToken))
        {
            return Results.NotFound("Agent with such Id is not found");
        }
        
        var agentProperties = await _propertyRepository.GetManyByConditionAsync(
                PropertyExpressionFilters.FilterByAgentId(agentId), cancellationToken);
        
        var agentPropertiesList = _mapper.Map<List<PropertyListedDto>>(agentProperties);
        
        return Results.Ok(agentPropertiesList);
    }
        
    public async Task<IResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(id, cancellationToken);
        
        if (property is null)
        {
            return Results.NotFound("The property is not found");
        }
        
        var propertyMapped = _mapper.Map<PropertyDetailsDto>(property);
        
        return Results.Ok(propertyMapped);
    }
}