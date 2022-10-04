using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests.Product;
using Onebrb.API.Models.Responses.Product;
using Onebrb.Application.Enums;
using Onebrb.Application.Products.Commands;
using Onebrb.Application.Products.Models;
using Onebrb.Application.Products.Queries;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProductResponseModel>> GetProductAsync(string productId)
        {
            ProductModel? product = await _mediator.Send(new GetProductByIdQuery() { Id = productId });

            if (product is null)
                return NotFound();

            return _mapper.Map<ProductResponseModel>(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProductResponseModel>> PostProductAsync(ProductRequestModel request)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null)
                return Unauthorized();

            ProductInputModel productInput = _mapper.Map<ProductInputModel>(request);
            productInput.AuthorId = currentUserId;

            ProductModel? product = await _mediator.Send(new PostProductCommand() { Product = productInput });

            if (product is null)
                return NotFound();

            return _mapper.Map<ProductResponseModel>(product);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteProductAsync(string productId)
        {
            // Get current user
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null)
                return Unauthorized();

            var result = await _mediator.Send(new DeleteProductByIdCommand() { ProductId = productId, CurrentUserId = currentUserId });

            if (result.Status == HandlerStatus.Failure)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
