using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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
using Onebrb.Application.Users.Queries.GetCountAllCommentsByUserId;
using Onebrb.Application.Users.Queries.GetUserById;
using Onebrb.Application.Users.Queries.GetUserProfileById;
using Onebrb.Application.Users.Queries.GetUserRatingByUserId;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
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

        public class MyEnableQueryAttribute : EnableQueryAttribute
        {
            public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
            {
                int pagesize = 5;
                var result = queryOptions.ApplyTo(queryable, new ODataQuerySettings { PageSize = pagesize });
                return result;
            }
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
            string? currentFirstName = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string? currentLastName = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;

            UserProfileModel? existingUser = await _mediator.Send(new GetUserProfileByIdQuery() { UserId = currentUserId });

            if (existingUser is not null)
                return Ok(_mapper.Map<UserProfileResponseModel>(existingUser));

            var user = new ActivateUserProfileModel
            {
                Id = currentUserId,
                Email = currentUserEmail,
                Name = currentUserName,
                FirstName = currentFirstName,
                LastName = currentLastName
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

            if (currentUserId is null) return Unauthorized();

            var existingUser = await _mediator.Send(new GetUserByIdQuery() { CurrentUserId = currentUserId, UserId = currentUserId });

            if (existingUser is null)
            {
                // Create user
                string currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
                string currentUserName = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;
                string currentFirstName = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
                string currentLastName = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;

                var userModel = new ActivateUserProfileModel
                {
                    Id = currentUserId,
                    Email = currentUserEmail,
                    Name = currentUserName,
                    FirstName = currentFirstName,
                    LastName = currentLastName
                };

                existingUser = await _mediator.Send(new ActivateUserCommand() { ProfileModel = userModel });
            }

            return Ok(_mapper.Map<UserProfileResponseModel>(existingUser));
        }

        [HttpGet]
        [Route("{userId}/comments")]
        [EnableQuery(PageSize = 5)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IQueryable<CommentResponseModel>>> GetCommentsAsync([FromRoute][Required] string userId, [FromQuery][Required] int top, [FromQuery][Required] int skip)
        {
            IQueryable<CommentModel> comments = await _mediator.Send(new GetAllCommentsByUserIdQuery() { Id = userId });

            return Ok(comments.ProjectTo<CommentResponseModel>(_mapper.ConfigurationProvider));
        }

        [HttpGet]
        [Route("{userId}/comments/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentsCountResponseModel>> GetCommentsCountAsync([FromRoute][Required] string userId)
        {
            int commentsCount = await _mediator.Send(new GetCountAllCommentsByUserIdQuery() { Id = userId });

            return Ok(new CommentsCountResponseModel { Count = commentsCount });
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
