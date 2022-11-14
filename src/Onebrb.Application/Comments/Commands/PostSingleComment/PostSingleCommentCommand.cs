using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProfileAggregate;
using Onebrb.Domain.Exceptions.Comment;

namespace Onebrb.Application.Comments.Commands.PostSingleCommentCommand;

public record PostSingleCommentCommand : IRequest<CommentModel>
{
    public PostCommentModel Comment { get; set; }
}

public class PostCommentCommandHandler : IRequestHandler<PostSingleCommentCommand, CommentModel>
{
    private readonly IGenericRepository<CommentEntity> _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostCommentCommandHandler(IGenericRepository<CommentEntity> commentRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentModel> Handle(PostSingleCommentCommand request, CancellationToken cancellationToken)
    {
        string trackId = Guid.NewGuid().ToString();

        _commentRepository.Insert(new CommentEntity
        {
            AuthorId = request.Comment.AuthorId,
            RecipientId = request.Comment.RecipientId,
            Content = request.Comment.Content,
            TrackId = trackId,
            Rating = new RatingEntity
            {
                Value = request.Comment.Rating,
                AuthorId = request.Comment.AuthorId,
                RecipientId = request.Comment.RecipientId,
            },
        });

        int result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new CommentCreateException("Couldn't create new post.");

        CommentEntity? comment = await _commentRepository.GetSingleOrDefault(p => p.TrackId == trackId);

        return _mapper.Map<CommentModel>(comment);
    }
}