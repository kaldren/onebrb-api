namespace Onebrb.Domain.Entities.ProfileAggregate;

public class RatingEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the rating
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the author id
    /// </summary>
    public string AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the recipient id
    /// </summary>
    public string RecipientId { get; set; }
}
