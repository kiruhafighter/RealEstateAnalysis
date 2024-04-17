using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class ImageRepository : BaseRepository<Image, int>, IImageRepository
{
    public ImageRepository(DbContext context) : base(context)
    {
        }
}