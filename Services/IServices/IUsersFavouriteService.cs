using Microsoft.AspNetCore.Http;

namespace Services.IServices
{
    public interface IUsersFavouriteService
    {
        Task<IResult> AddFavouriteAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken);
        
        Task<IResult> RemoveFavouriteAsync(Guid userId, int usersFavouriteId, CancellationToken cancellationToken);
        
        Task<IResult> GetFavouritesAsync(Guid userId, CancellationToken cancellationToken);
    }
}