using AutoMapper;
using Domain.Entities;
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
    private readonly IImageRepository _imageRepository;

    public PropertyService(IMapper mapper, IPropertyRepository propertyRepository, 
        IAgentRepository agentRepository, IImageRepository imageRepository)
    {
        _mapper = mapper;
        _propertyRepository = propertyRepository;
        _agentRepository = agentRepository;
        _imageRepository = imageRepository;
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
    
    public async Task<IResult> AddAsync(Guid userId, AddPropertyDto addPropertyInfo, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
        
        if (agent is null)
        {
            return Results.BadRequest("Agent account is not found");
        }
        
        var property = _mapper.Map<Property>(addPropertyInfo);
        property.AgentId = agent.Id;
           
        var addedProperty = await _propertyRepository.AddAsync(property, cancellationToken);
        
        if (!addedProperty)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        var images = _mapper.Map<List<Image>>(addPropertyInfo.Images);
        
        images.ForEach(i => i.PropertyId = property.Id);
        
        var addedImages = await _imageRepository.AddRangeAsync(images, cancellationToken);
        
        if (!addedImages)
        {
            return Results.Ok("Images failed to be added");
        }
        
        return Results.Ok();
    }

    public async Task<IResult> UpdateAsync(Guid userId, UpdatePropertyDto updatePropertyInfo, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
        
        if (agent is null)
        {
            return Results.BadRequest("Agent account is not found");
        }

        var propertyExisting = await _propertyRepository.GetByIdAsync(updatePropertyInfo.Id, cancellationToken);

        if (propertyExisting is null)
        {
            return Results.NotFound("Property is not found");
        }

        if (!propertyExisting.AgentId.Equals(agent.Id))
        {
            return Results.Forbid();
        }

        var propertyUpdateModel = _mapper.Map<Property>(updatePropertyInfo);

        if (!await _propertyRepository.UpdatePropertyAsync(propertyUpdateModel, cancellationToken))
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }
}