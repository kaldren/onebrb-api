using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserProfileModel?>
{
    public string CurrentUserId { get; set; }
}

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserProfileModel?>
{
    private readonly IGenericRepository<UserEntity> _profileRepository;
    private readonly IGenericRepository<RatingEntity> _ratingRepository;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IGenericRepository<UserEntity> profileRepository,
        IGenericRepository<RatingEntity> ratingRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileModel?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        UserEntity? profile = await _profileRepository.GetSingleOrDefault(p => p.Id == request.CurrentUserId);

        if (profile is null)
            return null;

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