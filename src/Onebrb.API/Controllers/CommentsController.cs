using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<GetSingleCommentByIdModel>>> GetCommentAsync([FromRoute] long commentId)
        {
            var res = await _mediator.Send(new GetSingleCommentByIdQuery() { Id = commentId });

            if (res is null)
                return NotFound();

            return Ok(res);
        }

        [HttpGet("users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<GetAllCommentsByUserIdModel>>> GetCommentsAsync([FromQuery] long userId)
        {
            var res = await _mediator.Send(new GetAllCommentsByUserIdQuery() { Id = userId });

            if (res is null)
                return NotFound();

            return Ok(res);
        }
    }
}
