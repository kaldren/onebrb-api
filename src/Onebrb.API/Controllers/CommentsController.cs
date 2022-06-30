using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Domain.Profile;
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

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<Comment>>> GetCommentsAsync([FromQuery] long recipientId)
        {
            var comments = await _commentService.GetCommentsAsync(recipientId);

            return Ok(comments);
        }
    }
}
