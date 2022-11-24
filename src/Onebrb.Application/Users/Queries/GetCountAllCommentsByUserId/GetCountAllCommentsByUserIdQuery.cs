using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Users.Queries.GetCountAllCommentsByUserId;

public record GetCountAllCommentsByUserIdQuery : IRequest<int>
{
    public string Id { get; set; }
}

public class GetCountAllCommentsByUserIdQueryHandler : IRequestHandler<GetCountAllCommentsByUserIdQuery, int>
{
    private readonly IGenericRepository<CommentEntity> _commentsRepository;
    private readonly IMapper _mapper;

    public GetCountAllCommentsByUserIdQueryHandler(IGenericRepository<CommentEntity> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(GetCountAllCommentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _commentsRepository.CountAsync(p => p.Recipient.Id == request.Id && p.IsDeleted == false);
    }
}