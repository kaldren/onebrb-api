using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Comment?> GetCommentAsync(long id)
            => await commentRepository.GetAsync(id);

        public async Task<ICollection<Comment>> GetCommentsAsync(long recipientId)
            => await commentRepository.GetAsync(p => p.Recipient.Id == recipientId);
    }
}
