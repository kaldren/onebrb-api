using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProfileAggregate;
using Onebrb.Domain.Exceptions.Comment;

namespace Onebrb.Application.Comments.Commands.DeleteSingleComment;

public record DeleteSingleCommentCommand : IRequest
{
    public string CommentId { get; set; }
}

public class DeleteSingleCommentCommandHandler : IRequestHandler<DeleteSingleCommentCommand>
{
    private readonly IGenericRepository<CommentEntity> _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSingleCommentCommandHandler(IGenericRepository<CommentEntity> commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteSingleCommentCommand request, CancellationToken cancellationToken)
    {
        // Validation here
        CommentEntity? comment = await _commentRepository.GetSingleOrDefault(p => p.Id == request.CommentId);
        if (comment == null)
            throw new CommentDeleteException($"Couldn't find comment with id {request.CommentId}");

        _commentRepository.Delete(comment);

        int result = await _unitOfWork.SaveChangesAsync();

        if (result < 1)
            throw new CommentDeleteException($"Couldn't delete comment with id {request.CommentId}");

        return Unit.Value;
    }
}
