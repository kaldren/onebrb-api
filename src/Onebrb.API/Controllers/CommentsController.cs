using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Responses;
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
    }
}
