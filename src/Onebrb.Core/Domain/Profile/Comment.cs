namespace Onebrb.Core.Domain.Profile
{
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        public Profile Author { get; set; }

        /// <summary>
        /// Gets or sets the recipient
        /// </summary>
        public Profile Recipient { get; set; }

        /// <summary>
        /// Gets or sets the date of posting
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
