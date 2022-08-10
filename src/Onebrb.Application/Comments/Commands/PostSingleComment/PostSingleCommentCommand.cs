using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Exceptions;

namespace Onebrb.Application.Comments.Commands.PostSingleCommentCommand;

public record PostSingleCommentCommand : IRequest<CommentModel>
{
    public PostCommentModel RequestModel { get; set; }
}

public class PostCommentCommandHandler : IRequestHandler<PostSingleCommentCommand, CommentModel>
{
    private readonly IGenericRepository<Comment> _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostCommentCommandHandler(IGenericRepository<Comment> profileRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _commentRepository = profileRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentModel> Handle(PostSingleCommentCommand request, CancellationToken cancellationToken)
    {
        string trackId = Guid.NewGuid().ToString();

        _commentRepository.Insert(new Comment
        {
            AuthorId = request.RequestModel.AuthorId,
            RecipientId = request.RequestModel.RecipientId,
            Content = request.RequestModel.Content,
            Date = DateTimeOffset.Now,
            TrackId = trackId
        });

        int result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new CommentException("Couldn't create new post.");

        Comment? comment = await _commentRepository.GetSingleOrDefault(p => p.TrackId == trackId);

        return _mapper.Map<CommentModel>(comment);
    }
}