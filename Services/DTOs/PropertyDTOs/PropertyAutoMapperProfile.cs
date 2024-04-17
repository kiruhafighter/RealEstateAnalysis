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
    }
}