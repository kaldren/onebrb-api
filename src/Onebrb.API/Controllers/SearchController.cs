using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Onebrb.API.Models.Responses.Profile;
using Onebrb.API.Models.Responses.Search;
using Onebrb.Application.Search.Queries.GetCountAllSearchResultsByFullName;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetAllUsersSearch;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IQueryable<UserProfileResponseModel>>> SearchProfilesAsync([FromQuery][Required] string fullName, [FromQuery][Required] int top, [FromQuery][Required] int skip)
        {
            IQueryable<UserProfileModel> comments = await _mediator.Send(new GetAllUsersSearchQuery() { FullName = fullName });

            return Ok(comments.ProjectTo<UserProfileResponseModel>(_mapper.ConfigurationProvider));
        }


        [HttpGet]
        [Route("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SearchCountResponseModel>> GetCommentsCountAsync([FromQuery][Required] string fullName)
        {
            int profilesCount = await _mediator.Send(new GetCountAllSearchResultsByFullNameQuery() { FullName = fullName });

            return Ok(new SearchCountResponseModel { Count = profilesCount });
        }
    }
}