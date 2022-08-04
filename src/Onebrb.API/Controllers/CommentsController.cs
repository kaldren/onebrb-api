using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Responses;
using Onebrb.Application.Comments.Commands.PostSingleComment;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetUserProfileById;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentResponseModel>> GetCommentAsync([FromRoute] string commentId)
        {
            CommentModel comment = await _mediator.Send(new GetSingleCommentByCommentIdQuery() { Id = commentId });

            if (comment is null)
                return NotFound();

            return Ok(_mapper.Map<CommentResponseModel>(comment));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentResponseModel>> PostCommentAsync([FromBody] PostCommentRequestModel comment)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            UserProfileModel? existingUser = await _mediator.Send(new GetUserProfileByIdQuery() { Id = currentUserId });

            if (existingUser is null)
                return BadRequest();

            PostCommentModel model = _mapper.Map<PostCommentModel>(comment);
            model.AuthorId = currentUserId;

            CommentModel result = await _mediator.Send(new PostCommentCommand() { RequestModel = model });

            return Ok(result);
        }
    }
}
