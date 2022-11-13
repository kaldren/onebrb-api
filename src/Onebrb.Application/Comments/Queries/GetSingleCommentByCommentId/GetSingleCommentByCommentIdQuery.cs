using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;

public record GetSingleCommentByCommentIdQuery : IRequest<CommentModel>
{
    public string Id { get; set; }
}

public class GetSingleCommentByCommentIdQueryHandler : IRequestHandler<GetSingleCommentByCommentIdQuery, CommentModel>
{
    private readonly IGenericRepository<CommentEntity> _commentsRepository;
    private readonly IMapper _mapper;

    public GetSingleCommentByCommentIdQueryHandler(IGenericRepository<CommentEntity> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<CommentModel> Handle(GetSingleCommentByCommentIdQuery request, CancellationToken cancellationToken)
    {
        CommentEntity? commentEntity = await _commentsRepository.GetAsync(request.Id);

        return _mapper.Map<CommentModel>(commentEntity);
    }
}
