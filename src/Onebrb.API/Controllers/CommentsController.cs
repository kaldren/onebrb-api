using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Application.Comments;
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
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentsController(ICommentService commentService, IMapper mapper, IMediator mediator)
        {
            _commentService = commentService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<GetSingleCommentByIdModel>>> GetCommentsAsync([FromQuery] long recipientId)
        {
            var res = await _mediator.Send(new GetSingleCommentByIdQuery() { Id = recipientId });

            if (res is null)
                return NotFound();

            return Ok(res);
        }
    }
}
