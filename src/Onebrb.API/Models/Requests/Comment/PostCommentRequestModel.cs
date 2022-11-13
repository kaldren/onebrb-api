namespace Onebrb.API.Models.Requests
{
    public class PostCommentRequestModel
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

        /// <summary>
        /// Gets or sets the rating
        /// </summary>
        public int Rating { get; set; }
    }
}
