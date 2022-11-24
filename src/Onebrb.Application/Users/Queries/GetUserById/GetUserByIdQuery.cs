using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery : IRequest<UserProfileModel?>
{
    public string CurrentUserId { get; set; }
    public string UserId { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserProfileModel?>
{
    private readonly IGenericRepository<UserEntity> _profileRepository;
    private readonly IGenericRepository<RatingEntity> _ratingRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        IGenericRepository<UserEntity> profileRepository,
        IGenericRepository<RatingEntity> ratingRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserEntity? user = await _profileRepository.GetSingleOrDefault(p => p.Id == request.UserId);

        if (user is null)
            return null;

        // Check for hidden settings
        if (user.Id != request.CurrentUserId)
        {
            //user.Phone = user.IsPhoneVisible.HasValue && user.IsPhoneVisible.Value is true && !string.IsNullOrEmpty(user.Phone) ? user.Phone : default;
            user.Email = user.IsEmailVisible && !string.IsNullOrEmpty(user.Email) ? user.Email : default;
        }

        var userProfileModel = _mapper.Map<UserProfileModel>(user);

        var ratings = (await _ratingRepository.GetAsync(p => p.RecipientId == user.Id)).ToList();

        if (ratings.Any())
        {
            var ratingSum = ratings.Sum(p => p.Value);
            var ratingCount = ratings.Count;
            userProfileModel.Rating = Math.Round(decimal.ToDouble(decimal.Divide(ratingSum, ratingCount)), 2);
        }

        return userProfileModel;
    }
}