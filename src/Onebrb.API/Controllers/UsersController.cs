using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetAllCommentsByUserId;
using Onebrb.Application.Users.Queries.GetUserProfileById;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsersController(
            IMapper mapper,
            IMediator mediator
        )
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<UserProfileModel>> GetUserAsync(int userId)
        {
            var res = await _mediator.Send(new GetUserProfileByIdQuery() { Id = userId });

            if (res == null) return NotFound();

            return Ok(res);
        }

        [HttpGet]
        [Route("{userId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<CommentModel>>> GetCommentsAsync([FromRoute] long userId)
        {
            var res = await _mediator.Send(new GetAllCommentsByUserIdQuery() { Id = userId });

            return Ok(res);
        }

        [HttpPatch]
        public async Task<ActionResult> EditProfileAsync([FromBody][Required] Domain.Entities.Profile.Profile profileModel)
        {
            return null;
        }
    }
}
