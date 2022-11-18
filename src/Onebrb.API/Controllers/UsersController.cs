using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Responses.Comment;
using Onebrb.API.Models.Responses.Occupation;
using Onebrb.API.Models.Responses.Profile;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Occupations.Models;
using Onebrb.Application.Occupations.Queries;
using Onebrb.Application.Users.Commands;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetAllCommentsByUserId;
using Onebrb.Application.Users.Queries.GetCurrentUser;
using Onebrb.Application.Users.Queries.GetUserProfileById;
using Onebrb.Application.Users.Queries.GetUserRatingByUserId;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<UserProfileResponseModel>> GetUserAsync(string userId)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null) return Unauthorized();

            UserProfileModel? user = await _mediator.Send(new GetUserProfileByIdQuery() { UserId = userId, CurrentUserId = currentUserId });

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserProfileResponseModel>(user));
        }

        [HttpGet("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileResponseModel>> ActivateUserAsync()
        {
            // TODO: Check if user already created

            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            string? currentUserName = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;

            UserProfileModel? existingUser = await _mediator.Send(new GetUserProfileByIdQuery() { UserId = currentUserId });

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileResponseModel>> GetCurrentlyAuthenticatedUserAsync()
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null) return NotFound();

            UserProfileModel? user = await _mediator.Send(new GetCurrentUserQuery() { CurrentUserId = currentUserId });

            if (user is null)
                return NotFound();

            return Ok(_mapper.Map<UserProfileResponseModel>(user));
        }

        [HttpGet]
        [Route("{userId}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<CommentResponseModel>>> GetCommentsAsync([FromRoute][Required] string userId)
        {
            ICollection<CommentModel> comments = await _mediator.Send(new GetAllCommentsByUserIdQuery() { Id = userId });

            return Ok(_mapper.Map<ICollection<CommentResponseModel>>(comments));
        }

        [HttpGet("{userId}/rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<UserRatingResponseModel>> GetUserRatingAsync(string userId)
        {
            UserRatingModel rating = await _mediator.Send(new GetUserRatingByUserIdQuery() { Id = userId });
            return this.Ok(_mapper.Map<UserRatingResponseModel>(rating));
        }

        [HttpGet("{userId}/occupation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OccupationResponseModel>> Get(string userId)
        {
            OccupationModel? occupation = await _mediator.Send(new GetUserOccupationQuery() { UserId = userId });

            if (occupation is null)
                return NotFound();

            return Ok(_mapper.Map<OccupationResponseModel>(occupation));
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileResponseModel>> UpdateProfileAsync([FromBody][Required] UpdateUserProfileRequestModel profile)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            if (currentUserId is null) return Unauthorized();

            if (currentUserId != profile.Id) return Unauthorized();

            UpdateUserProfileModel profileModel = _mapper.Map<UpdateUserProfileModel>(profile);

            UserProfileModel? user = await _mediator.Send(new UpdateUserProfileCommand() { ProfileModel = profileModel });

            if (user is null) return NotFound();

            return this.Ok(_mapper.Map<UserProfileResponseModel>(user));
        }
    }
}
