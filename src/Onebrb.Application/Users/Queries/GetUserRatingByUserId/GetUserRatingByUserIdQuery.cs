using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Users.Queries.GetUserRatingByUserId;

public record GetUserRatingByUserIdQuery : IRequest<UserRatingModel>
{
    public string Id { get; set; }
}

public class GetUserRatingByUserIdQueryHandler : IRequestHandler<GetUserRatingByUserIdQuery, UserRatingModel>
{
    private readonly IGenericRepository<ProfileEntity> _profileRepository;
    private readonly IGenericRepository<RatingEntity> _ratingRepository;
    private readonly IMapper _mapper;

    public GetUserRatingByUserIdQueryHandler(
        IGenericRepository<ProfileEntity> profileRepository,
        IGenericRepository<RatingEntity> ratingRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }

    public async Task<UserRatingModel> Handle(GetUserRatingByUserIdQuery request, CancellationToken cancellationToken)
    {
        UserRatingModel model = new();
        ProfileEntity? profile = await _profileRepository.GetSingleOrDefault(p => p.Id == request.Id);

        if (profile is null)
            return model;

        model.UserId = profile.Id;

        var ratings = (await _ratingRepository.GetAsync(p => p.RecipientId == profile.Id)).ToList();

        if (ratings.Any())
        {
            var ratingSum = ratings.Sum(p => p.Rating);
            var ratingCount = ratings.Count;
            model.Rating = Math.Round(decimal.ToDouble(decimal.Divide(ratingSum, ratingCount)), 2);
        }

        return model;
    }
}