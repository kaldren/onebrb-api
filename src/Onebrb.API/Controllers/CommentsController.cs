using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Requests.Comment;
using Onebrb.API.Models.Responses.Comment;
using Onebrb.Application.Comments.Commands.DeleteSingleComment;
using Onebrb.Application.Comments.Commands.PatchSingleComment;
using Onebrb.Application.Comments.Commands.PostSingleComment;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetAllCommentsByUserId;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;
using Onebrb.Application.Users.Queries.GetUserExistByUserId;
using Onebrb.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    [RequiredScope("Onebrb.Read")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAzureQueueService _azureQueueService;
        private readonly INotificationsService _notificationsService;

        public CommentsController(IMediator mediator, IMapper mapper, IAzureQueueService azureQueueService, INotificationsService notificationsService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _azureQueueService = azureQueueService;
            _notificationsService = notificationsService;
        }

        #region GET
        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponseModel>> GetCommentAsync([FromRoute][Required] string commentId)
        {
            CommentModel comment = await _mediator.Send(new GetSingleCommentByCommentIdQuery() { Id = commentId });

            if (comment is null)
                return NotFound();

            return Ok(_mapper.Map<CommentResponseModel>(comment));
        }

        /// <summary>
        /// Gets all comments between users
        /// </summary>
        /// <param name="recipientId">The recipient id</param>
        /// <param name="authorId">The sender id</param>
        /// <returns>All comments or empty collection.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<CommentResponseModel>>> GetCommentsFromSenderAsync([FromQuery][Required] string recipientId, [FromQuery][Required] string authorId)
        {
            ICollection<CommentModel> comments = await _mediator.Send(new GetAllCommentsFromSenderToRecipientQuery() { RecipientId = recipientId, AuthorId = authorId });

            return Ok(_mapper.Map<ICollection<CommentResponseModel>>(comments));
        }

        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponseModel>> PostCommentAsync([FromBody][Required] PostCommentRequestModel comment)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            string? firstNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string? lastNameClaim = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;

            bool existingUser = await _mediator.Send(new GetUserExistByUserIdQueryQuery() { CurrentUserId = currentUserId });

            //if (existingUser == false)
            //    return NotFound("The user doesn't exist.");

            PostCommentModel model = _mapper.Map<PostCommentModel>(comment);
            model.AuthorId = currentUserId;

            ICollection<CommentModel> comments = await _mediator.Send(new GetAllCommentsFromSenderToRecipientQuery() { RecipientId = comment.RecipientId, AuthorId = comment.AuthorId });

            if (comments.Any())
                return BadRequest("User already posted a comment to this user.");

            CommentModel? result = await _mediator.Send(new PostSingleCommentCommand() { Comment = model });

            if (result != null)
            {
                //await _azureQueueService.InsertAsync("new-comment-notification-queue", "");

                // Notify
                await _notificationsService.Notify(comment.RecipientId, $"You have received a new comment from {firstNameClaim} {lastNameClaim}.", "ToastNotification");
            }

            return Ok(this._mapper.Map<CommentResponseModel>(result));
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponseModel>> PatchCommentAsync([FromBody][Required] PatchCommentRequestModel comment)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            bool existingUser = await _mediator.Send(new GetUserExistByUserIdQueryQuery() { CurrentUserId = currentUserId });

            if (existingUser is false)
                return NotFound();

            PatchCommentModel model = _mapper.Map<PatchCommentModel>(comment);
            model.AuthorId = currentUserId;

            CommentModel result = await _mediator.Send(new PatchSingleCommentCommand() { RequestModel = model });

            return Ok(this._mapper.Map<CommentResponseModel>(result));
        }
        #endregion

        #region DELETE
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCommentAsync([FromRoute][Required] string commentId)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            CommentModel comment = await _mediator.Send(new GetSingleCommentByCommentIdQuery() { Id = commentId });

            if (comment is null)
                return NotFound();

            if (currentUserId != comment.AuthorId)
                return Unauthorized();

            await _mediator.Send(new DeleteSingleCommentCommand() { CommentId = commentId });

            return Ok();
        }
        #endregion
    }
}
