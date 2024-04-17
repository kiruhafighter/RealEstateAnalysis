using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.ImageDTOs;

public sealed class ImageAutoMapperProfile : Profile
{
    public ImageAutoMapperProfile()
    {
        CreateMap<Image, ImageListedDto>();
    }
}