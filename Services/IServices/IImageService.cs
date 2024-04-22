using Microsoft.AspNetCore.Http;
using Services.DTOs.ImageDTOs;

namespace Services.IServices
{
    public interface IImageService
    {
        Task<IResult> AddImageForProperty(Guid userId, Guid propertyId, AddImageDto imageAddInfo,
            CancellationToken cancellationToken);

        Task<IResult> DeleteImageForProperty(Guid userId, Guid propertyId, int imageId,
            CancellationToken cancellationToken);
    }
}