using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Users.Queries.GetUserProfileById;

public record GetUserProfileByIdQuery : IRequest<UserProfileModel?>
{
    public string CurrentUserId { get; set; }

    public string UserId { get; set; }
}

public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileModel?>
{
    private readonly IGenericRepository<ProfileEntity> _profileRepository;
    private readonly IGenericRepository<RatingEntity> _ratingRepository;
    private readonly IMapper _mapper;

    public GetUserProfileByIdQueryHandler(
        IGenericRepository<ProfileEntity> profileRepository,
        IGenericRepository<RatingEntity> ratingRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileModel?> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        ProfileEntity? currentUser = await _profileRepository.GetSingleOrDefault(p => p.Id == request.CurrentUserId);

        if (currentUser is null)
            return null;

        ProfileEntity? profile = await _profileRepository.GetSingleOrDefault(p => p.Id == request.UserId);

        if (profile is null)
            return null;

        // Check for hidden settings
        if (currentUser.Id != profile.Id)
        {
            profile.Phone = profile.IsPhoneVisible && !string.IsNullOrEmpty(profile.Phone) ? profile.Phone : default;
            profile.Email = profile.IsEmailVisible && !string.IsNullOrEmpty(profile.Email) ? profile.Email : default;
        }

        var userProfileModel = _mapper.Map<UserProfileModel>(profile);

        var ratings = (await _ratingRepository.GetAsync(p => p.RecipientId == profile.Id)).ToList();

        if (ratings.Any())
        {
            var ratingSum = ratings.Sum(p => p.Value);
            var ratingCount = ratings.Count;
            userProfileModel.Rating = Math.Round(decimal.ToDouble(decimal.Divide(ratingSum, ratingCount)), 2);
        }

        return userProfileModel;
    }
}