using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.UsersFavouriteDTOs
{
    public sealed class UsersFavouriteAutoMapperProfile : Profile
    {
        public UsersFavouriteAutoMapperProfile()
        {
            CreateMap<UsersFavourite, UsersFavouriteListedDto>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property!.Name));
        }
    }
}