using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Interfaces;

namespace Onebrb.Application.Comments.Queries;

public record GetSingleCommentByCommentIdQuery : IRequest<GetSingleCommentByCommentIdModel>
{
    public long Id { get; set; }
}

public class GetSingleCommentByCommentIdQueryHandler : IRequestHandler<GetSingleCommentByCommentIdQuery, GetSingleCommentByCommentIdModel>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetSingleCommentByCommentIdQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<GetSingleCommentByCommentIdModel> Handle(GetSingleCommentByCommentIdQuery request, CancellationToken cancellationToken)
    {
        Comment? commentEntity = await _commentsRepository.GetAsync(request.Id);

        return _mapper.Map<GetSingleCommentByCommentIdModel>(commentEntity);
    }
}
