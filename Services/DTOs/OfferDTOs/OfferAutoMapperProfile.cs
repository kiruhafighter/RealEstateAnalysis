using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.OfferDTOs;

internal sealed class OfferAutoMapperProfile : Profile
{
    public OfferAutoMapperProfile()
    {
        CreateMap<AddOfferDto, Offer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.OfferDate, opt => opt.Ignore())
            .ForMember(dest => dest.OfferStatusId, opt => opt.Ignore())
            .ForMember(dest => dest.OfferStatus, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}