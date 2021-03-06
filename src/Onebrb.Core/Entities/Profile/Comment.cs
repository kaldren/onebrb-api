using System.ComponentModel.DataAnnotations.Schema;

namespace Onebrb.Domain.Entities.Profile
{
    public class Comment : BaseEntity
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
        /// Gets or sets the author
        /// </summary>
        public virtual Profile Author { get; set; }

        /// <summary>
        /// Gets or sets the recipient id
        /// </summary>
        public string RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the recipient
        [ForeignKey(nameof(RecipientId))]
        public virtual Profile Recipient { get; set; }

        /// <summary>
        /// Gets or sets the date of posting
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
