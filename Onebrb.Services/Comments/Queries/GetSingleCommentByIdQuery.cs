using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Interfaces;

namespace Onebrb.Application.Comments.Queries;

public record GetSingleCommentByIdQuery : IRequest<GetSingleCommentByIdModel>
{
    public long Id { get; set; }
}

public class GetSingleCommentByIdQueryHandler : IRequestHandler<GetSingleCommentByIdQuery, GetSingleCommentByIdModel>
{
    private readonly IGenericRepository<Comment> _commentsRepository;
    private readonly IMapper _mapper;

    public GetSingleCommentByIdQueryHandler(IGenericRepository<Comment> commentsRepository, IMapper mapper)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
    }

    public async Task<GetSingleCommentByIdModel> Handle(GetSingleCommentByIdQuery request, CancellationToken cancellationToken)
    {
        Comment? commentEntity = await _commentsRepository.GetAsync(request.Id);

        return _mapper.Map<GetSingleCommentByIdModel>(commentEntity);
    }
}
