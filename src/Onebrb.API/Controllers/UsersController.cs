using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Identity.Web.Resource;
using Onebrb.API.Models.Requests.User;
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
using Onebrb.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Onebrb.API.Controllers
{
    [RequiredScope("Onebrb.Read")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAzureImageStoreService _azureImageStoreService;

        public UsersController(
            IMapper mapper,
            IMediator mediator,
            IAzureImageStoreService azureImageStoreService
        )
        {
            _mapper = mapper;
            _mediator = mediator;
            _azureImageStoreService = azureImageStoreService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<UserProfileResponseModel>> GetUserAsync(string userId)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

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
            string? userIdClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            string? userEmailClaim = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            string? userNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;
            string? firstNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string? lastNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            string? jobClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/job")?.Value;

            UserProfileModel? existingUser = await _mediator.Send(new GetUserProfileByIdQuery() { UserId = userIdClaim });

            if (existingUser is not null)
                return Ok(_mapper.Map<UserProfileResponseModel>(existingUser));

            var user = new ActivateUserProfileModel
            {
                Id = userIdClaim,
                Email = userEmailClaim,
                Name = userNameClaim,
                FirstName = firstNameClaim,
                LastName = lastNameClaim
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

            UserProfileModel? existingUser = await _mediator.Send(new GetUserByIdQuery() { CurrentUserId = currentUserId, UserId = currentUserId });

            if (existingUser is null)
            {
                var allClaims = User?.Claims.ToList();

                // Create user
                string? emailClaim = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
                string? usernameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;
                string? firstNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
                string? lastNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
                string? countryClaim = User?.Claims?.FirstOrDefault(x => x.Type == "country")?.Value;

                var userModel = new ActivateUserProfileModel
                {
                    Id = currentUserId,
                    Email = emailClaim,
                    Name = usernameClaim,
                    FirstName = firstNameClaim,
                    LastName = lastNameClaim,
                    Country = countryClaim
                };

                existingUser = await _mediator.Send(new ActivateUserCommand() { ProfileModel = userModel });

                if (existingUser is null)
                {
                    throw new Exception("Couldn't create profile");
                }
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

            if (currentUserId != profile.Id) return Unauthorized();

            UpdateUserProfileModel profileModel = _mapper.Map<UpdateUserProfileModel>(profile);

            // Add image to Blob storage
            if (profile.Photo is not null)
            {
                var matchGroups = Regex.Match(profile.Photo, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
                var base64Data = matchGroups["data"].Value;

                byte[] data = System.Convert.FromBase64String(base64Data);
                bool validImage = _azureImageStoreService.IsImage(data);
                if (!validImage)
                {
                    return BadRequest("Invalid image file.");
                }

                await _azureImageStoreService.StoreImageAsync(profile.Id, profile.Id, data);
                profileModel.HasPhoto = true;
            }

            UserProfileModel? user = await _mediator.Send(new UpdateUserProfileCommand() { ProfileModel = profileModel });

            if (user is null) return NotFound();

            return this.Ok(_mapper.Map<UserProfileResponseModel>(user));
        }
    }
}
