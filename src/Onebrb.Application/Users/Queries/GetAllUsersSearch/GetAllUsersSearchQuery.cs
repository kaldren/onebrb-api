using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Users.Queries.GetAllUsersSearch;

public record GetAllUsersSearchQuery : IRequest<IQueryable<UserProfileModel>>
{
    public string FullName { get; set; }
}

public class GetAllUsersSearchQueryHandler : IRequestHandler<GetAllUsersSearchQuery, IQueryable<UserProfileModel>>
{
    private readonly IGenericRepository<ProfileEntity> _profilesRepository;
    private readonly IMapper _mapper;

    public GetAllUsersSearchQueryHandler(IGenericRepository<ProfileEntity> profilesRepository, IMapper mapper)
    {
        _profilesRepository = profilesRepository;
        _mapper = mapper;
    }

    public async Task<IQueryable<UserProfileModel>> Handle(GetAllUsersSearchQuery request, CancellationToken cancellationToken)
    {
        var comments = _profilesRepository.GetQueryableAsync(p => p.FullName.StartsWith(request.FullName)).ProjectTo<UserProfileModel>(_mapper.ConfigurationProvider);

        return comments;
    }
}
