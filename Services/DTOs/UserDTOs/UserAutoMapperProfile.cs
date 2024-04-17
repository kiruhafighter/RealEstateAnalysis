using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.UserDTOs;

internal sealed class UserAutoMapperProfile : Profile
{
    public UserAutoMapperProfile()
    {
            CreateMap<User, UserDetailsDto>();
        }
}