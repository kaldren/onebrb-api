using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Interfaces;

namespace Onebrb.Application.Comments.Queries;

public record GetAllCommentsByUserIdQuery : IRequest<ICollection<GetAllCommentsByUserIdModel>>
{
    public long Id { get; set; }
}

public class GetAllCommentsByRecipientIdQueryHandler : IRequestHandler<GetAllCommentsByUserIdQuery, ICollection<GetAllCommentsByUserIdModel>>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetAllCommentsByRecipientIdQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<GetAllCommentsByUserIdModel>> Handle(GetAllCommentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        ICollection<Comment> comments = await _commentsRepository.GetAsync(p => p.Recipient.Id == request.Id);

        return _mapper.Map<ICollection<GetAllCommentsByUserIdModel>>(comments);
    }
}
