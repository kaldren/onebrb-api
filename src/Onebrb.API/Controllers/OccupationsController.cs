using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Onebrb.API.Models.Responses.Occupation;
using Onebrb.Application.Occupations.Models;
using Onebrb.Application.Occupations.Queries;

namespace Onebrb.API.Controllers
{
    [RequiredScope("Onebrb.Read")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OccupationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<OccupationResponseModel>>> Get()
        {
            ICollection<OccupationModel> occupations = await _mediator.Send(new GetAllOccupationsQuery() { });

            if (occupations is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<OccupationResponseModel>>(occupations));
        }
    }
}
