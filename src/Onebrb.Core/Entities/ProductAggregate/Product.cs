using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Domain.Entities.ProductAggregate;

public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the product description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the product images
    /// </summary>
    public virtual ICollection<ProductImage>? Images { get; set; }

    /// <summary>
    /// Gets or sets the profile id
    /// </summary>
    public string ProfileId { get; set; }

    /// <summary>
    /// Gets or sets the profile
    /// </summary>
    public virtual Profile Profile { get; set; }
}
