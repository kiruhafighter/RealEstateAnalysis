using AutoMapper;
using Domain.Entities;

namespace Services.DTOs.AgentDTOs
{
    internal sealed class AgentAutoMapperProfile : Profile
    {
        public AgentAutoMapperProfile()
        {
            CreateMap<Agent, AgentDetailsDto>();
        }
    }
}