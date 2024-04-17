using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class UsersFavouriteRepository : BaseRepository<UsersFavourite, int>, IUsersFavouriteRepository
{
    public UsersFavouriteRepository(DbContext context) : base(context)
    {
        }
}