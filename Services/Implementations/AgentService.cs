using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs.AgentDTOs;
using Services.IServices;
using Role = Services.Enums.Role;

namespace Services.Implementations;

public class AgentService : IAgentService
{
    private readonly IAgentRepository _agentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AgentService(IAgentRepository agentRepository, IUserRepository userRepository, IMapper mapper)
    {
            _agentRepository = agentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
    public async Task<IResult> AddAgentAccountAsync(Guid userId, AddAgentDto addAgentDto, CancellationToken cancellationToken)
    {
        if (await _agentRepository.AgentExistsByUserIdAsync(userId, cancellationToken))
        {
            return Results.BadRequest("Agent already exists for you");
        }

        if (await _agentRepository.AgentExistsByEmailAsync(addAgentDto.Email, cancellationToken))
        {
            return Results.BadRequest("Agent with such email already exists");
        }

        var agent = new Agent
        {
            Id = Guid.NewGuid(),
            AgencyName = addAgentDto.AgencyName,
            FirstName = addAgentDto.FirstName,
            LastName = addAgentDto.LastName,
            Email = addAgentDto.Email,
            PhoneNumber = addAgentDto.PhoneNumber,
            UserId = userId
        };
            
        var added = await _agentRepository.AddAsync(agent, cancellationToken);
            
        if (!added)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
            
        var updateUserRole = await _userRepository.UpdateUserRoleAsync(userId, (int)Role.Agent, cancellationToken);
            
        if (!updateUserRole)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
            
        return Results.Ok();
    }

    public async Task<IResult> GetAgentAccountDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetByIdAsync(id, cancellationToken);
            
        if (agent is null)
        {
            Results.NotFound("Agent is not found");
        }
            
        var agentDetails = _mapper.Map<AgentDetailsDto>(agent);
            
        return Results.Ok(agentDetails);
    }
        
    public async Task<IResult> GetAgentByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
            
        if (agent is null)
        {
            Results.NotFound("Agent is not found");
        }
            
        var agentDetails = _mapper.Map<AgentDetailsDto>(agent);
            
        return Results.Ok(agentDetails);
    }
        
    public async Task<IResult> UpdateAgentInfoAsync(Guid userId, UpdateAgentDto updateInfo, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetAgentByUserIdAsync(userId, cancellationToken);
            
        if (agent is null)
        {
            Results.NotFound("Agent is not found");
        }

        if (!agent!.Email.Equals(updateInfo.Email) &&
            !await _agentRepository.AgentExistsByEmailAsync(updateInfo.Email, cancellationToken))
        {
            return Results.BadRequest("Agent with such email already exists");
        }

        var agentUpdated = new Agent
        {
            Id = agent.Id,
            Email = updateInfo.Email,
            FirstName = updateInfo.FirstName,
            LastName = updateInfo.LastName,
            AgencyName = updateInfo.AgencyName,
            PhoneNumber = updateInfo.PhoneNumber
        };

        var updateSucceeded = await _agentRepository.UpdateAgentAsync(agentUpdated, cancellationToken);

        if (!updateSucceeded)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
            
        return Results.Ok();
    }
}