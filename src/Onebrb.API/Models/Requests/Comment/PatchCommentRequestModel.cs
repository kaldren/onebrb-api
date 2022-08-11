namespace Onebrb.API.Models.Requests.Comment
{
    public class PatchCommentRequestModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

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
