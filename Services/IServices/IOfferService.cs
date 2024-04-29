using Microsoft.AspNetCore.Http;
using Services.DTOs.OfferDTOs;

namespace Services.IServices;

public interface IOfferService
{
    Task<IResult> AddOfferAsync(Guid userId, AddOfferDto addOfferDto, CancellationToken cancellationToken);
    
    Task<IResult> GetOfferByIdAsync(Guid offerId, CancellationToken cancellationToken);
    
    Task<IResult> GetOffersByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<IResult> GetOffersByPropertyIdAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken);
    
    Task<IResult> GetUserOffersForPropertyAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken);
    
    Task<IResult> UpdateOfferAsync(Guid userId, UpdateOfferDto updateOfferDto, CancellationToken cancellationToken);
    
    Task<IResult> DeleteOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken);
    
    Task<IResult> ApproveOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken);
    
    Task<IResult> RejectOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken);
}