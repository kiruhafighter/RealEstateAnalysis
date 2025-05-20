using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.UsersFavouriteDTOs
{
    public sealed class UsersFavouriteAutoMapperProfile : Profile
    {
        public UsersFavouriteAutoMapperProfile()
        {
            CreateMap<UsersFavourite, UsersFavouriteListedDto>()
                .ForMember(dest => dest.PropertyName, 
                    opt => opt.MapFrom(src => src.Property!.Name))
                .ForMember(dest => dest.FirstImage,
                    opt => opt.MapFrom(src => src.Property!.Images.Select(i => i.ImagePath).FirstOrDefault()));
        }
    }
}