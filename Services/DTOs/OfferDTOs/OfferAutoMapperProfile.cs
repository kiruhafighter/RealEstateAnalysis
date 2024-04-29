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

        CreateMap<Offer, OfferDetailsDto>()
            .ForMember(dest => dest.OfferStatusName, 
                opt => opt.MapFrom(src => src.OfferStatus!.StatusName))
            .ForMember(dest => dest.PropertyName, 
                opt => opt.MapFrom(src => src.Property!.Name))
            .ForMember(dest => dest.UserFullName,
                opt => opt.MapFrom(src => src.User!.FirstName + " " + src.User!.LastName))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User!.Email));
        
        CreateMap<Offer, OfferListedDto>()
            .ForMember(dest => dest.OfferStatusName, 
                opt => opt.MapFrom(src => src.OfferStatus!.StatusName))
            .ForMember(dest => dest.PropertyName, 
                opt => opt.MapFrom(src => src.Property!.Name))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User!.Email));
    }
}