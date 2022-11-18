using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Occupations.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Occupations.Queries;

public record GetAllOccupationsQuery : IRequest<ICollection<OccupationModel>>
{
}

public class GetAllOccupationsQueryHandler : IRequestHandler<GetAllOccupationsQuery, ICollection<OccupationModel>>
{
    private readonly IGenericRepository<OccupationEntity> _occupationsRepository;
    private readonly IMapper _mapper;

    public GetAllOccupationsQueryHandler(IGenericRepository<OccupationEntity> occupationEntity, IMapper mapper)
    {
        _occupationsRepository = occupationEntity;
        _mapper = mapper;
    }

    public async Task<ICollection<OccupationModel>> Handle(GetAllOccupationsQuery request, CancellationToken cancellationToken)
    {
        ICollection<OccupationEntity>? occupationEntities = await _occupationsRepository.GetAsync(p => p.IsDeleted == false);

        return _mapper.Map<ICollection<OccupationModel>>(occupationEntities);
    }
}
