using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs.ImageDTOs;
using Services.IServices;

namespace Services.Implementations;

public sealed class ImageService : IImageService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAgentRepository _agentRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public ImageService(IPropertyRepository propertyRepository,
        IAgentRepository agentRepository,
        IImageRepository imageRepository,
        IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _agentRepository = agentRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddImageForProperty(Guid userId, Guid propertyId, AddImageDto imageAddInfo, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        
        if (property is null)
        {
            return Results.NotFound("Property is not found");
        }
        
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
        
        if (agent is null)
        {
            return Results.BadRequest("Agent is not found");
        }
        
        if (!property.AgentId.Equals(agent.Id))
        {
            return Results.Forbid();
        }
        
        var image = _mapper.Map<Image>(imageAddInfo);
        image.PropertyId = propertyId;
        
        if (!await _imageRepository.AddAsync(image, cancellationToken))
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> DeleteImageForProperty(Guid userId, Guid propertyId, int imageId, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        
        if (property is null)
        {
            return Results.NotFound("Property is not found");
        }
        
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
        
        if (agent is null)
        {
            return Results.BadRequest("Agent is not found");
        }
        
        if (!property.AgentId.Equals(agent.Id))
        {
            return Results.Forbid();
        }
        
        if (!property.Images.Exists(i => i.Id == imageId))
        {
            return Results.NotFound("Image is not found");
        }
        
        if (!await _imageRepository.DeleteAsync(imageId, cancellationToken))
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }
}