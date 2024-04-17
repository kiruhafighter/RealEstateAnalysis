using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace DataAccess.Repositories;

internal sealed class OfferRepository : BaseRepository<Offer, Guid>, IOfferRepository
{
    public OfferRepository(DbContext context) : base(context)
    {
        }
}