using Habr.DataAccess.Constraints;
using Habr.Services;
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

        [HttpPost]
        [Route("~/api/posts/{postId}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCommentAsync([FromRoute] int postId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            [FromHeader] int userId)
        {
            try
            {
                await _commentService.AddCommentAsync(text, postId, userId);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("~/api/posts/{postId}/comments/{commentId}/reply")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReplyToCommentAsync([FromRoute] int postId,
            [FromRoute] int commentId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            [FromHeader] int userId)
        {
            try
            {
                await _commentService.ReplyToCommentAsync(text, commentId, postId, userId);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditCommentAsync([FromBody] string newText, [FromRoute] int id, [FromHeader] int userId)
        {
            try
            {
                await _commentService.ModifyCommentAsync(newText, id, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id, [FromHeader] int userId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id, userId);
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
