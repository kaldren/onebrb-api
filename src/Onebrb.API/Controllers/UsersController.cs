using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Commands;
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
        public async Task<ActionResult<UserProfileResponseModel>> GetUserAsync(string userId)
        {
            UserProfileModel user = await _mediator.Send(new GetUserProfileByIdQuery() { Id = userId });

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserProfileResponseModel>(user));
        }

        [HttpGet("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileResponseModel>> ActivateUserAsync()
        {
            // TODO: Check if user already created

            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            string? currentUserName = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;

            UserProfileModel? existingUser = await _mediator.Send(new GetUserProfileByIdQuery() { Id = currentUserId });

            if (existingUser is not null)
                return Ok(_mapper.Map<UserProfileResponseModel>(existingUser));

            var user = new ActivateUserProfileModel
            {
                Id = currentUserId,
                Email = currentUserEmail,
                Name = currentUserName
            };

            UserProfileModel? activatedUser = await _mediator.Send(new ActivateUserCommand() { ProfileModel = user });

            if (activatedUser is null) return NotFound();

            return Ok(_mapper.Map<UserProfileResponseModel>(activatedUser));
        }

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileResponseModel>> GetCurrentlyAuthenticatedUserAsync()
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null) return NotFound();

            UserProfileModel user = await _mediator.Send(new GetUserProfileByIdQuery() { Id = currentUserId });

            if (user is null)
                return NotFound();

            return Ok(_mapper.Map<UserProfileResponseModel>(user));
        }

        [HttpGet]
        [Route("{userId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<CommentResponseModel>>> GetCommentsAsync([FromRoute] string userId)
        {
            ICollection<CommentModel> comments = await _mediator.Send(new GetAllCommentsByUserIdQuery() { Id = userId });

            return Ok(_mapper.Map<ICollection<CommentResponseModel>>(comments));
        }

        [HttpPatch]
        public async Task<ActionResult> EditProfileAsync([FromBody][Required] Domain.Entities.Profile.Profile profileModel)
        {
            return null;
        }
    }
}
