using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Responses;
using Onebrb.Application.Comments.Commands.PostSingleCommentCommand;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetUserProfileById;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Lazy<IMediator> _mediator;
        private readonly Lazy<IMapper> _mapper;

        public CommentsController(Lazy<IMediator> mediator, Lazy<IMapper> mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentResponseModel>> GetCommentAsync([FromRoute][Required] string commentId)
        {
            CommentModel comment = await _mediator.Value.Send(new GetSingleCommentByCommentIdQuery() { Id = commentId });

            if (comment is null)
                return NotFound();

            return Ok(_mapper.Value.Map<CommentResponseModel>(comment));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentResponseModel>> PostCommentAsync([FromBody][Required] PostCommentRequestModel comment)
        {
            string? currentUserId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

            UserProfileModel? existingUser = await _mediator.Value.Send(new GetUserProfileByIdQuery() { Id = currentUserId });

            if (existingUser is null)
                return BadRequest();

            PostCommentModel model = _mapper.Value.Map<PostCommentModel>(comment);
            model.AuthorId = currentUserId;

            CommentModel result = await _mediator.Value.Send(new PostSingleCommentCommand() { RequestModel = model });

            return Ok(result);
        }
    }
}
