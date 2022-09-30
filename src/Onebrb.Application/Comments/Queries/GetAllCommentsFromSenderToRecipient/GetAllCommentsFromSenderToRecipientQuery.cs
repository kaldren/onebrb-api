using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Comments.Queries.GetAllCommentsByUserId;

public record GetAllCommentsFromSenderToRecipientQuery : IRequest<ICollection<CommentModel>>
{
    public string RecipientId { get; set; }

    public string AuthorId { get; set; }
}

public class GetAllCommentsFromSenderToRecipientQueryHandler : IRequestHandler<GetAllCommentsFromSenderToRecipientQuery, ICollection<CommentModel>>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetAllCommentsFromSenderToRecipientQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CommentModel>> Handle(GetAllCommentsFromSenderToRecipientQuery request, CancellationToken cancellationToken)
    {
        ICollection<Comment> comments = await _commentsRepository.GetAsync(p => p.Recipient.Id == request.RecipientId && p.AuthorId == request.AuthorId);

        return _mapper.Map<ICollection<CommentModel>>(comments);
    }
}
