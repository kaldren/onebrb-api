using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Search.Queries.GetCountAllSearchResultsByFullName;

public record GetCountAllSearchResultsByFullNameQuery : IRequest<int>
{
    public string FullName { get; set; }
}

public class GetCountAllSearchResultsByFullNameQueryHandler : IRequestHandler<GetCountAllSearchResultsByFullNameQuery, int>
{
    private readonly IGenericRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;

    public GetCountAllSearchResultsByFullNameQueryHandler(IGenericRepository<UserEntity> commentsRepository, IMapper mapper)
    {
        _usersRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(GetCountAllSearchResultsByFullNameQuery request, CancellationToken cancellationToken)
    {
        return await _usersRepository.CountAsync(p => p.FullName.StartsWith(request.FullName));
    }
}
