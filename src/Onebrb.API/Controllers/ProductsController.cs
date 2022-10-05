using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests.Product;
using Onebrb.API.Models.Responses.Product;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseModel>> PostProductAsync(ProductPostRequestModel request)
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

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseModel>> PatchProductAsync(ProductPatchRequestModel request)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null)
                return Unauthorized();

            ProductInputModel productInput = _mapper.Map<ProductInputModel>(request);
            productInput.AuthorId = currentUserId;

            ProductModel? product = await _mediator.Send(new PatchProductCommand() { Product = productInput });

            if (product is null)
                return NotFound();

            return _mapper.Map<ProductResponseModel>(product);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProductAsync(string productId)
        {
            // Get current user
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null)
                return Unauthorized();

            var product = await _mediator.Send(new GetProductByIdQuery() { Id = productId });

            if (product is null)
                return NotFound();

            var result = await _mediator.Send(new DeleteProductByIdCommand() { ProductId = productId, CurrentUserId = currentUserId });

            return Ok();
        }
    }
}
