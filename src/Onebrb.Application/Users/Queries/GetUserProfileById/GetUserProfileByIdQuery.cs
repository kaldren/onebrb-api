using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;

namespace Onebrb.Application.Users.Queries.GetUserProfileById;

public record GetUserProfileByIdQuery : IRequest<UserProfileModel>
{
    public string Id { get; set; }
}

public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileModel>
{
    private readonly IGenericRepository<Domain.Entities.ProfileAggregate.ProfileEntity> _profileRepository;
    private readonly IMapper _mapper;

    public GetUserProfileByIdQueryHandler(IGenericRepository<Domain.Entities.ProfileAggregate.ProfileEntity> profileRepository, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileModel> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.ProfileAggregate.ProfileEntity? profile = await _profileRepository.GetSingleOrDefault(p => p.Id == request.Id);

        return _mapper.Map<UserProfileModel>(profile);
    }
}