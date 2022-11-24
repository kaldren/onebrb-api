using AutoMapper;
using MediatR;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.UserAggregate;
using Onebrb.Domain.Exceptions.Comment;

namespace Onebrb.Application.Comments.Commands.PatchSingleComment;

public record PatchSingleCommentCommand : IRequest<CommentModel>
{
    public PatchCommentModel RequestModel { get; set; }
}

public class PatchCommentCommandHandler : IRequestHandler<PatchSingleCommentCommand, CommentModel>
{
    private readonly IGenericRepository<CommentEntity> _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatchCommentCommandHandler(IGenericRepository<CommentEntity> commentRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentModel> Handle(PatchSingleCommentCommand request, CancellationToken cancellationToken)
    {
        CommentEntity? currentComment = await _commentRepository.GetSingleOrDefault(p => p.Id == request.RequestModel.Id);

        if (currentComment is null)
            throw new CommentNotFoundException($"Comment with id {request.RequestModel.Id} does not exist.");

        currentComment.Content = request.RequestModel.Content;
        //currentComment.DateUpdated = DateTimeOffset.UtcNow;
        _commentRepository.Update(currentComment);

        int result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new CommentUpdateException($"Couldn't update comment with id {request.RequestModel.Id}");

        CommentEntity? updatedComment = await _commentRepository.GetSingleOrDefault(p => p.Id == request.RequestModel.Id);

        return _mapper.Map<CommentModel>(updatedComment);
    }
}