using System.ComponentModel.DataAnnotations.Schema;

namespace Onebrb.Domain.Entities.ProfileAggregate;

public class CommentEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author id
    /// </summary>
    public string AuthorId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author
    /// </summary>
    public virtual ProfileEntity? Author { get; set; }

    /// <summary>
    /// Gets or sets the recipient id
    /// </summary>
    public string RecipientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient
    [ForeignKey(nameof(RecipientId))]
    public virtual ProfileEntity? Recipient { get; set; }

    /// <summary>
    /// Gets or sets the rating
    public virtual RatingEntity? Rating { get; set; }
}
