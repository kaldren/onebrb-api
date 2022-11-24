namespace Onebrb.Domain.Entities.UserAggregate;

public class RatingEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the rating
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Gets or sets the author id
    /// </summary>
    public string AuthorId { get; set; }

    public virtual UserEntity Author { get; set; }

    /// <summary>
    /// Gets or sets the recipient id
    /// </summary>
    public string RecipientId { get; set; }

    public virtual UserEntity Recipient { get; set; }
}
