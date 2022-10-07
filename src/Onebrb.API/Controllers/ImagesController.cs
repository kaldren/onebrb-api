using FileTypeChecker;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ImagesController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
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

            for (int i = 0; i < files.Length; i++)
            {
                // Create directory if not exist
                Directory.CreateDirectory($"C:\\temp\\{currentUserId}\\{productId}\\");

                //Strip out any path specifiers (ex: /../)
                var originalFileName = Path.GetFileName(files[0].FileName);

                //Create a unique file path
                var uniqueFileName = Path.GetRandomFileName();
                var uniqueFilePath = Path.Combine(@$"C:\temp\{currentUserId}\{productId}\", uniqueFileName);

                //Save the file to disk
                //using (var stream = System.IO.File.Create(uniqueFilePath))
                //{
                //    await files[0].CopyToAsync(stream);
                //}
                using (var fileStream = new FileStream(uniqueFilePath, FileMode.Create))
                {
                    var inputStream = files[0].OpenReadStream();

                    // Check if it is image
                    bool isJpeg = FileTypeValidator.Is<JointPhotographicExpertsGroup>(files[0].OpenReadStream());

                    if (!isJpeg)
                        return BadRequest("Invalid file format. Allowed JPEG only.");

                    else
                        await inputStream.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }
    }
}
