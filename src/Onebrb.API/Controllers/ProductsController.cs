using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Responses;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponseModel>> GetProductAsync(string productId)
        {
            return null;
        }
    }
}
