using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;

namespace Onebrb.Application.Users.Queries.GetUserProfileById;

public record GetUserProfileByIdQuery : IRequest<UserProfileModel>
{
    public long Id { get; set; }
}

internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileModel>
{
    private readonly IGenericRepository<Domain.Entities.Profile.Profile> _profileRepository;
    private readonly IMapper _mapper;

    public GetUserProfileByIdQueryHandler(IGenericRepository<Domain.Entities.Profile.Profile> profileRepository, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileModel> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.Profile.Profile? profile = await _profileRepository.GetAsync(request.Id);

        return _mapper.Map<UserProfileModel>(profile);
    }
}