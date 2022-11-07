using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests.Product;
using Onebrb.API.Models.Responses.Product;
using Onebrb.Application.Enums;
using Onebrb.Application.Products.Commands;
using Onebrb.Application.Products.Models;
using Onebrb.Application.Products.Queries;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAzureQueueService _azureQueueService;

        public ProductsController(IMapper mapper, IMediator mediator, IAzureQueueService azureQueueService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _azureQueueService = azureQueueService;
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

        [HttpGet("{productId}/images/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetProductImagesCountAsync(string productId)
        {
            ProductModel? product = await _mediator.Send(new GetProductByIdQuery() { Id = productId });

            if (product is null)
                return NotFound();

            return Ok(await _mediator.Send(new GetProductImagesCountQuery() { Id = productId }));
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutProductInterestAsync()
        {
            await _azureQueueService.InsertAsync("notifications", "test message");

            return Ok();
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

            var productUpdateResult = await _mediator.Send(new PatchProductCommand() { Product = productInput });

            if (productUpdateResult.Status == HandlerStatus.Failure)
                return BadRequest(productUpdateResult.Message);

            return _mapper.Map<ProductResponseModel>(productUpdateResult.Result);
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
