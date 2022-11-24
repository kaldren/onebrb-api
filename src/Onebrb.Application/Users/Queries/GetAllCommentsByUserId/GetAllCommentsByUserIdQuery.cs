using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application.Users.Queries.GetAllCommentsByUserId;

public record GetAllCommentsByUserIdQuery : IRequest<IQueryable<CommentModel>>
{
    public string Id { get; set; }
}

public class GetAllCommentsByUserIdQueryHandler : IRequestHandler<GetAllCommentsByUserIdQuery, IQueryable<CommentModel>>
{
    private readonly IGenericRepository<CommentEntity> _commentsRepository;
    private readonly IMapper _mapper;

    public GetAllCommentsByUserIdQueryHandler(IGenericRepository<CommentEntity> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<IQueryable<CommentModel>> Handle(GetAllCommentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var comments = _commentsRepository.GetQueryableAsync(p => p.Recipient.Id == request.Id).ProjectTo<CommentModel>(_mapper.ConfigurationProvider);

        return comments;
    }
}
