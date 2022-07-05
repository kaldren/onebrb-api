using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models;
using Onebrb.Core.Services.Comments;

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

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<CommentResponseModel>>> GetCommentsAsync([FromQuery] long recipientId)
        {
            var comments = await _commentService.GetCommentsAsync(recipientId);

            var res = _mapper.Map<ICollection<CommentResponseModel>>(comments);

            return Ok(res);
        }
    }
}
