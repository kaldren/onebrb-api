using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries;
using Onebrb.Application.Users.Queries.GetAllCommentsByUserId;
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

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Domain.Entities.Profile.Profile>> GetCurrentlyAuthenticatedUserAsync()
        {
            //string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;

            //if (currentUserEmail is null)
            //    return Unauthorized();

            //var profile = await _profileService.GetProfileAsync(currentUserEmail);

            //if (profile == null)
            //    return NotFound();

            //return Ok(profile);
            return null;
        }

        [HttpPost("profiles/activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ActivatedUserProfileResponseModel>> ActivateUserProfileAsync()
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            string? currentUserName = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;

            UserProfileModel profile = await _mediator.Send(new Application.Users.Commands.ActivateUserProfileCommand()
            {
                ProfileModel = new ActivateUserProfileModel
                {
                    Id = currentUserId,
                    Email = currentUserEmail,
                    Name = currentUserName
                }
            });

            if (profile == null) return NotFound();

            return Ok(_mapper.Map<ActivatedUserProfileResponseModel>(profile));
        }

        [HttpPatch]
        public async Task<ActionResult> EditProfileAsync([FromBody][Required] Domain.Entities.Profile.Profile profileModel)
        {
            //string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;

            //if (currentUserEmail is null || profileModel?.Email != currentUserEmail)
            //    return Unauthorized();

            //var profile = await _profileService.UpdateProfileAsync(profileModel);

            //if (profile == null)
            //    return NotFound();

            //return Ok(profile);
            return null;
        }
    }
}
