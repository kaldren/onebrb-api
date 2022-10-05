namespace Onebrb.Application.Products.Models;

public class ProductInputModel
{
    /// <summary>
    /// Gets or sets the product id
    /// </summary>
    public string Id { get; set; }

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
    /// Gets or sets the product's author id
    /// </summary>
    public string AuthorId { get; set; }
}
