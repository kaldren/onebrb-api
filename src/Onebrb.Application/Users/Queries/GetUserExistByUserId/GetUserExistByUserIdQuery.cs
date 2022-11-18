using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Users.Queries.GetUserExistByUserId;

public record GetUserExistByUserIdQueryQuery : IRequest<bool>
{
    public string CurrentUserId { get; set; }
}

public class GetUserExistByUserIdQueryQueryHandler : IRequestHandler<GetUserExistByUserIdQueryQuery, bool>
{
    private readonly IGenericRepository<ProfileEntity> _profileRepository;

    public GetUserExistByUserIdQueryQueryHandler(
        IGenericRepository<ProfileEntity> profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<bool> Handle(GetUserExistByUserIdQueryQuery request, CancellationToken cancellationToken)
    {
        ProfileEntity? profile = await _profileRepository.GetSingleOrDefault(p => p.Id == request.CurrentUserId);

        return profile is not null;
    }
}
