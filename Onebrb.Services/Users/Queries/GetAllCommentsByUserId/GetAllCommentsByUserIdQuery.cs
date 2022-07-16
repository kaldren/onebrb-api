using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Users.Queries.GetAllCommentsByUserId;

public record GetAllCommentsByUserIdQuery : IRequest<ICollection<CommentModel>>
{
    public long Id { get; set; }
}

internal class GetAllCommentsByUserIdQueryHandler : IRequestHandler<GetAllCommentsByUserIdQuery, ICollection<CommentModel>>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetAllCommentsByUserIdQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CommentModel>> Handle(GetAllCommentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        ICollection<Comment> comments = await _commentsRepository.GetAsync(p => p.Recipient.Id == request.Id);

        return _mapper.Map<ICollection<CommentModel>>(comments);
    }
}
