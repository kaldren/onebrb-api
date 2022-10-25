using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Models.Responses.Comment;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    public class ConversationsController : ControllerBase
    {
        #region GET
        [HttpGet("{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponseModel>> GetConversationAsync([FromRoute][Required] string conversationId)
        {
            //CommentModel comment = await _mediator.Send(new GetSingleCommentByCommentIdQuery() { Id = conversationId });

            //if (comment is null)
            //    return NotFound();

            //return Ok(_mapper.Map<CommentResponseModel>(comment));
            return null;
        }
        #endregion
    }
}
