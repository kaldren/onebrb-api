using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Responses;
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
    }
}
