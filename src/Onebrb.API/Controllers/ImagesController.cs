using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProductAggregate;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IAzureImageStoreService _azureImageStore;

        public ImagesController(IGenericRepository<Product> productRepository, IAzureImageStoreService azureImageStore)
        {
            _productRepository = productRepository;
            _azureImageStore = azureImageStore;
        }

        [HttpPost("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PostImagesAsync(string productId, IFormFile[] files)
        {
            if (files.Length <= 0)
                return BadRequest("Empty file");

            // Get user id
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            // Get product
            var product = await _productRepository.GetSingleOrDefault(p => p.Id == productId);

            if (product is null)
                return NotFound($"Product with id {productId} not found.");

            if (product.ProfileId != currentUserId)
            {
                return Unauthorized();
            }

            // Scan the file for viruses

            // Store to Azure
            for (int i = 0; i < files.Length; i++)
            {
                await _azureImageStore.StoreImageAsync(product.Id, product.Id, files[i]);
            }

            return Ok();
        }
    }
}
