using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Onebrb.API.Models.Responses.Profile;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetAllUsersSearch;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SearchController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery(PageSize = 5)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IQueryable<UserProfileResponseModel>>> SearchProfilesAsync([FromQuery][Required] string fullName)
        {
            IQueryable<UserProfileModel> comments = await _mediator.Send(new GetAllUsersSearchQuery() { FullName = fullName });

            return Ok(comments.ProjectTo<UserProfileResponseModel>(_mapper.ConfigurationProvider));
        }
    }
}