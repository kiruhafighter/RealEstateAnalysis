using Microsoft.AspNetCore.Http;
using Services.DTOs.UserDTOs;

namespace Services.IServices
{
    public interface IUserService
    {
        Task<IResult> RegisterAsync(AddUserDto user, CancellationToken cancellationToken);
        
        Task<IResult> LoginAsync(UserLoginDto request, CancellationToken cancellationToken);
        
        Task<IResult> GetUserDetails(Guid id, CancellationToken cancellationToken);
        
        Task<IResult> ChangeUserDetails(Guid id, UpdateUserInfoDto updatedInfo, CancellationToken cancellationToken);
    }
}