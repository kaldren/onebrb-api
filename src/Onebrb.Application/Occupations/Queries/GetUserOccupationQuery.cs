using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Occupations.Models;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Occupations.Queries;

public record GetUserOccupationQuery : IRequest<OccupationModel>
{
    public string UserId { get; set; }
}

public class GetUserOccupationQueryHandler : IRequestHandler<GetUserOccupationQuery, OccupationModel>
{
    private readonly IGenericRepository<OccupationEntity> _occupationsRepository;
    private readonly IGenericRepository<UserEntity> _profileRepository;
    private readonly IMapper _mapper;

    public GetUserOccupationQueryHandler(
        IGenericRepository<OccupationEntity> occupationRepository,
        IGenericRepository<UserEntity> profileRepository,
        IMapper mapper)
    {
        _occupationsRepository = occupationRepository;
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<OccupationModel?> Handle(GetUserOccupationQuery request, CancellationToken cancellationToken)
    {
        UserEntity profileEntity = await _profileRepository.GetSingleOrDefault(p => p.Id == request.UserId);

        if (profileEntity is null)
        {
            return null;
        }

        return _mapper.Map<OccupationModel>(profileEntity.Occupation);
    }
}