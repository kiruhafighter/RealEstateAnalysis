using Domain.Entities;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class ImageRepository : BaseRepository<Image, int>, IImageRepository
{
    public ImageRepository(RealEstateDBContext context) : base(context)
    {
    }
}