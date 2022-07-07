using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Interfaces;

namespace Onebrb.Application.Users.Queries;

public record GetAllCommentsByUserIdQuery : IRequest<ICollection<CommentModel>>
{
    public long Id { get; set; }
}

public class GetAllCommentsByUserIdQueryHandler : IRequestHandler<GetAllCommentsByUserIdQuery, ICollection<CommentModel>>
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
