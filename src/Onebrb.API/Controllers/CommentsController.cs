using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CommentModel>> GetCommentAsync([FromRoute] long commentId)
        {
            var res = await _mediator.Send(new GetSingleCommentByCommentIdQuery() { Id = commentId });

            if (res is null)
                return NotFound();

            return Ok(res);
        }
    }
}
