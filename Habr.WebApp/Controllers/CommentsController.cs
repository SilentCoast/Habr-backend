using Habr.DataAccess.Constraints;
using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCommentAsync([FromBody] CommentAddModel model, [FromHeader] int userId)
        {
            try
            {
                await _commentService.AddCommentAsync(model.Text, model.PostId, userId);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reply")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReplyToCommentAsync([FromBody] CommentReplyModel model, [FromHeader] int userId)
        {
            try
            {
                await _commentService.ReplyToCommentAsync(model.Text, model.ParrentCommentId, model.PostId, userId);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditComment([FromHeader] string newText, [FromHeader] int commentId, [FromHeader] int userId)
        {
            try
            {
                await _commentService.ModifyCommentAsync(newText, commentId, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCommentAsync([FromHeader] int commentId, [FromHeader] int userId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
