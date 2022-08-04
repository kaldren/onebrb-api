namespace Onebrb.Application.Comments.Models
{
    public class PostCommentModel
    {
        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author id
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the recipient id
        /// </summary>
        public string RecipientId { get; set; }
    }
}
