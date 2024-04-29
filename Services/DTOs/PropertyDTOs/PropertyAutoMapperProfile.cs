using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.PropertyDTOs;

internal sealed class PropertyAutoMapperProfile : Profile
{
    public PropertyAutoMapperProfile()
    {
        CreateMap<Property, PropertyListedDto>()
            .ForMember(dest => dest.PropertyStatus, 
                opt => opt.MapFrom(src => src.PropertyStatus!.StatusName))
            .ForMember(dest => dest.PropertyType,
                opt => opt.MapFrom(src => src.PropertyType!.TypeName))
            .ForMember(dest => dest.FirstImage,
                opt => opt.MapFrom(src => src.Images.Select(i => i.ImagePath).FirstOrDefault()));

        CreateMap<Property, PropertyDetailsDto>()
            .ForMember(dest => dest.PropertyStatusName, 
                opt => opt.MapFrom(src => src.PropertyStatus!.StatusName))
            .ForMember(dest => dest.PropertyTypeName,
                opt => opt.MapFrom(src => src.PropertyType!.TypeName));

        CreateMap<AddPropertyDto, Property>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.AgentId, opt => opt.Ignore())
            .ForMember(dest => dest.Agent, opt => opt.Ignore())
            .ForMember(dest => dest.Offers, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyStatus, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyType, opt => opt.Ignore());
        
        CreateMap<UpdatePropertyDto, Property>()
            .ForMember(dest => dest.AgentId, opt => opt.Ignore())
            .ForMember(dest => dest.Agent, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyType, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.Offers, opt => opt.Ignore());
    }
}