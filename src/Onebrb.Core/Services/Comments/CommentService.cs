using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;

namespace Onebrb.Core.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Comment?> GetCommentAsync(long id)
            => await commentRepository.GetByIdAsync(id);

        public async Task<ICollection<Comment>> GetCommentsAsync(long recipientId)
            => await commentRepository.GetCollectionOrDefault(p => p.Recipient.Id == recipientId);
    }
}
