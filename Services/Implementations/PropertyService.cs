using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs;
using Services.DTOs.PropertyDTOs;
using Services.ExpressionFilters;
using Services.Extensions;
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

    public async Task<IResult> GetPropertiesFilteredAsync(FilterPropertiesRequest filterRequest, CancellationToken cancellationToken)
    {
        var (totalCount, properties) = await _propertyRepository.GetManyPagedAsync(filterRequest.PageNumber,
            filterRequest.PageSize,
            ExpressionExtensions.CombineExpressions(
                PropertyExpressionFilters.FilterByName(filterRequest.Name),
                PropertyExpressionFilters.FilterByAddress(filterRequest.Address),
                PropertyExpressionFilters.FilterByCounty(filterRequest.County),
                PropertyExpressionFilters.FilterByCountry(filterRequest.Country),
                PropertyExpressionFilters.FilterByLocality(filterRequest.Locality),
                PropertyExpressionFilters.FilterByNumberOfRooms(filterRequest.NumberOfRooms),
                PropertyExpressionFilters.FilterByNumberOfFloors(filterRequest.NumberOfFloors),
                PropertyExpressionFilters.FilterByPropertyTypeId(filterRequest.PropertyTypeId),
                PropertyExpressionFilters.FilterByYearBuilt(filterRequest.MinYearBuilt, filterRequest.MaxYearBuilt),
                PropertyExpressionFilters.FilterByPlotArea(filterRequest.MinPlotArea, filterRequest.MaxPlotArea),
                PropertyExpressionFilters.FilterByFloorArea(filterRequest.MinFloorArea, filterRequest.MaxFloorArea),
                PropertyExpressionFilters.FilterByPrice(filterRequest.MinPrice, filterRequest.MaxPrice)),
            cancellationToken);
        
        var propertiesList = _mapper.Map<List<PropertyListedDto>>(properties);
        
        return Results.Ok(new CollectionResult<PropertyListedDto>
        {
            Data = propertiesList,
            TotalCount = totalCount
        });
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
    
    public async Task<IResult> AddPropertyAsync(Guid userId, AddPropertyDto addPropertyInfo, CancellationToken cancellationToken)
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

    public async Task<IResult> GetAveragePricesForTimePeriodAsync(GetAveragePricesSampleRequest request, CancellationToken cancellationToken)
    {
        var periodStart = new DateTime(request.StartYear, request.StartMonth, 1);
        
        var periodEnd = new DateTime(request.EndYear, request.EndMonth + 1, 1).AddDays(-1);

        var currentMonthLastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
        
        if (periodStart > periodEnd || periodEnd > currentMonthLastDay)
        {
            return Results.BadRequest("Invalid time period");
        }
        
        var averagePrices = await _propertyRepository.GetAveragePriceForTimePeriodAsync(
            ExpressionExtensions.CombineExpressions(
                PropertyExpressionFilters.FilterByName(request.Name),
                PropertyExpressionFilters.FilterByAddress(request.Address),
                PropertyExpressionFilters.FilterByCounty(request.County),
                PropertyExpressionFilters.FilterByCountry(request.Country),
                PropertyExpressionFilters.FilterByLocality(request.Locality),
                PropertyExpressionFilters.FilterByNumberOfRooms(request.NumberOfRooms),
                PropertyExpressionFilters.FilterByNumberOfFloors(request.NumberOfFloors),
                PropertyExpressionFilters.FilterByPropertyTypeId(request.PropertyTypeId),
                PropertyExpressionFilters.FilterByYearBuilt(request.MinYearBuilt, request.MaxYearBuilt),
                PropertyExpressionFilters.FilterByPlotArea(request.MinPlotArea, request.MaxPlotArea),
                PropertyExpressionFilters.FilterByFloorArea(request.MinFloorArea, request.MaxFloorArea),
                PropertyExpressionFilters.FilterByPrice(request.MinPrice, request.MaxPrice)),
            periodStart, periodEnd, cancellationToken);
        
        return Results.Ok(averagePrices);
    }
}