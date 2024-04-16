using Microsoft.AspNetCore.Http;
using Services.DTOs.AgentDTOs;

namespace Services.IServices
{
    public interface IAgentService
    {
        Task<IResult> AddAgentAccountAsync(Guid userId, AddAgentDto addAgentDto, CancellationToken cancellationToken);

        Task<IResult> GetAgentAccountDetailsAsync(Guid id, CancellationToken cancellationToken);

        Task<IResult> GetAgentByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<IResult> UpdateAgentInfoAsync(Guid userId, UpdateAgentDto updateInfo, CancellationToken cancellationToken);
    }
}