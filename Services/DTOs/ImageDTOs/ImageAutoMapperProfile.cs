using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.ImageDTOs;

public sealed class ImageAutoMapperProfile : Profile
{
    public ImageAutoMapperProfile()
    {
        CreateMap<Image, ImageListedDto>();

        CreateMap<AddImageDto, Image>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore());
    }
}