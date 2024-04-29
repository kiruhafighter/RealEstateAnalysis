using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.DTOs.OfferDTOs;
using Services.IServices;
using OfferStatus = Services.Enums.OfferStatus;

namespace Services.Implementations;

public sealed class OfferService : IOfferService
{
    private readonly IMapper _mapper;
    private readonly IOfferRepository _offerRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public OfferService(IMapper mapper, IOfferRepository offerRepository, IPropertyRepository propertyRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _offerRepository = offerRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task<IResult> AddOfferAsync(Guid userId, AddOfferDto addOfferDto, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var property = await _propertyRepository.GetByIdAsync(addOfferDto.PropertyId, cancellationToken);
        
        if (property is null)
        {
            return Results.NotFound("Property is not found");
        }

        if (property.Agent!.UserId.Equals(userId))
        {
            return Results.BadRequest("You cannot do an offer for your own properties");
        }
        
        var offer = _mapper.Map<Offer>(addOfferDto);
        offer.UserId = userId;
        offer.OfferStatusId = (int)OfferStatus.Pending;
        
        var result = await _offerRepository.AddAsync(offer, cancellationToken);

        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> GetOfferByIdAsync(Guid offerId, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetByIdAsync(offerId, cancellationToken);
        
        if (offer is null)
        {
            return Results.NotFound("Offer is not found");
        }
        
        var offerDetails = _mapper.Map<OfferDetailsDto>(offer);
        
        return Results.Ok(offerDetails);
    }

    public async Task<IResult> GetOffersByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var offers = await _offerRepository.GetByUserIdAsync(userId, cancellationToken);
        
        var offersListed = _mapper.Map<IList<OfferListedDto>>(offers);
        
        return Results.Ok(offersListed);
    }

    public async Task<IResult> GetOffersByPropertyIdAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var property = await _propertyRepository.GetByIdAsync(propertyId, cancellationToken);
        
        if (property is null)
        {
            return Results.NotFound("Property is not found");
        }

        if (!property.Agent!.UserId.Equals(userId))
        {
            return Results.BadRequest("You are not authorized to see offers for this property");
        }
        
        var offers = await _offerRepository.GetByPropertyIdAsync(propertyId, cancellationToken);
        
        var offersListed = _mapper.Map<IList<OfferListedDto>>(offers);
        
        return Results.Ok(offersListed);
    }

    public async Task<IResult> GetUserOffersForPropertyAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        if (!await _propertyRepository.ExistsAsync(propertyId, cancellationToken))
        {
            return Results.NotFound("Property is not found");
        }
        
        var offers = await _offerRepository.GetByPropertyIdAndUserIdAsync(propertyId, userId, cancellationToken);
        
        var offersListed = _mapper.Map<IList<OfferListedDto>>(offers);
        
        return Results.Ok(offersListed);
    }

    public async Task<IResult> UpdateOfferAsync(Guid userId, UpdateOfferDto updateOfferDto, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var offer = await _offerRepository.GetByIdAsync(updateOfferDto.Id, cancellationToken);
        
        if (offer is null)
        {
            return Results.NotFound("Offer is not found");
        }
        
        if (!offer.UserId.Equals(userId))
        {
            return Results.BadRequest("You are not authorized to update this offer");
        }
        
        if (!offer.OfferStatusId.Equals((int)OfferStatus.Pending))
        {
            return Results.BadRequest("You cannot delete an offer that is not pending");
        }
        
        var result = await _offerRepository.UpdateOfferAsync(updateOfferDto.Id, updateOfferDto.OfferAmount, updateOfferDto.Comment, cancellationToken);
        
        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> DeleteOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var offer = await _offerRepository.GetByIdAsync(offerId, cancellationToken);
        
        if (offer is null)
        {
            return Results.NotFound("Offer is not found");
        }
        
        if (!offer.UserId.Equals(userId))
        {
            return Results.BadRequest("You are not authorized to update this offer");
        }

        if (!offer.OfferStatusId.Equals((int)OfferStatus.Pending))
        {
            return Results.BadRequest("You cannot delete an offer that is not pending");
        }
        
        var result = await _offerRepository.UpdateOfferStatusAsync(offerId, (int)OfferStatus.Inactive, cancellationToken);

        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> ApproveOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var offer = await _offerRepository.GetByIdAsync(offerId, cancellationToken);
        
        if (offer is null)
        {
            return Results.NotFound("Offer is not found");
        }
        
        if (!offer.Property!.Agent!.UserId.Equals(userId))
        {
            return Results.BadRequest("You are not authorized for this action");
        }
        
        if (!offer.OfferStatusId.Equals((int)OfferStatus.Pending))
        {
            return Results.BadRequest("You cannot approve an offer that is not pending");
        }
        
        var result = await _offerRepository.UpdateOfferStatusAsync(offerId, (int)OfferStatus.Active, cancellationToken);
        
        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }

    public async Task<IResult> RejectOfferAsync(Guid userId, Guid offerId, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsAsync(userId, cancellationToken))
        {
            return Results.NotFound("User is not found");
        }
        
        var offer = await _offerRepository.GetByIdAsync(offerId, cancellationToken);
        
        if (offer is null)
        {
            return Results.NotFound("Offer is not found");
        }
        
        if (!offer.Property!.Agent!.UserId.Equals(userId))
        {
            return Results.BadRequest("You are not authorized for this action");
        }
        
        if (!offer.OfferStatusId.Equals((int)OfferStatus.Pending))
        {
            return Results.BadRequest("You cannot reject an offer that is not pending");
        }
        
        var result = await _offerRepository.UpdateOfferStatusAsync(offerId, (int)OfferStatus.Rejected, cancellationToken);
        
        if (!result)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Results.Ok();
    }
}