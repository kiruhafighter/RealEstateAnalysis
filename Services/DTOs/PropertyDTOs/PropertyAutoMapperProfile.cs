using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.PropertyDTOs
{
    internal sealed class PropertyAutoMapperProfile : Profile
    {
        public PropertyAutoMapperProfile()
        {
            CreateMap<Property, PropertyListedDto>();
        }
    }
}