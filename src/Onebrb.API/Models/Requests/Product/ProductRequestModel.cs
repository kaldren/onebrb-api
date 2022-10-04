namespace Onebrb.API.Models.Requests.Product
{
    public class ProductRequestModel
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
    }
}
