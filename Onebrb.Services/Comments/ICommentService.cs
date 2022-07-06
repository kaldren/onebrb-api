using Onebrb.Core.Domain.Profile;

namespace Onebrb.Services.Comments
{
    public interface ICommentService
    {
        /// <summary>
        /// Gets a single comment
        /// </summary>
        /// <param name="id">The id of the comment</param>
        /// <returns>The comment or null if no comment is found</returns>
        Task<Comment?> GetCommentAsync(long id);

        /// <summary>
        /// Gets all comments by recipient id
        /// </summary>
        /// <param name="recipientId">The id of the recipient</param>
        /// <returns>All comments of the recipient or empty collection if no comments are found</returns>
        Task<ICollection<Comment>> GetCommentsAsync(long recipientId);
    }
}
