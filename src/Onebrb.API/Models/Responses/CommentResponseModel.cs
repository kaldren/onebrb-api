namespace Onebrb.API.Models.Responses
{
    public class CommentResponseModel
    {
        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author id
        /// </summary>
        public long AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author first name
        /// </summary>
        public string AuthorFirstName { get; set; }

        /// <summary>
        /// Gets or sets the author last name
        /// </summary>
        public string AuthorLastName { get; set; }

        /// <summary>
        /// Gets or sets the recipient id
        /// </summary>
        public long RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the recipient first name
        public string RecipientFirstName { get; set; }


        /// <summary>
        /// Gets or sets the recipient last name
        public string RecipientLastName { get; set; }

        /// <summary>
        /// Gets or sets the date of posting
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
