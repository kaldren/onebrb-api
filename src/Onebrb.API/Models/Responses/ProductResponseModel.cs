namespace Onebrb.API.Models.Responses
{
    public class ProductResponseModel
    {
        /// <summary>
        /// Gets or sets the product Id
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
        /// Gets or sets the product images
        /// </summary>
        public ICollection<ProductImageResponseModel> Images { get; set; }

        /// <summary>
        /// Gets or sets the date of posting
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
