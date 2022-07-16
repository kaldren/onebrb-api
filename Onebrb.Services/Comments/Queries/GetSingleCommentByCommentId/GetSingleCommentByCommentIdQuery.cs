using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;

public record GetSingleCommentByCommentIdQuery : IRequest<CommentModel>
{
    public long Id { get; set; }
}

public class GetSingleCommentByCommentIdQueryHandler : IRequestHandler<GetSingleCommentByCommentIdQuery, CommentModel>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetSingleCommentByCommentIdQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<CommentModel> Handle(GetSingleCommentByCommentIdQuery request, CancellationToken cancellationToken)
    {
        Comment? commentEntity = await _commentsRepository.GetAsync(request.Id);

        return _mapper.Map<CommentModel>(commentEntity);
    }
}
