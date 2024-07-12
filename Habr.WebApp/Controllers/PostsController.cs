using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> GetPost([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return Ok(await _postService.GetPostView(id, cancellationToken));
        }

        [HttpGet("published")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> GetPublishedPosts(CancellationToken cancellationToken = default)
        {
            return Ok(await _postService.GetPublishedPosts(cancellationToken));
        }

        [HttpGet("drafted")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> GetDraftedPosts(CancellationToken cancellationToken = default)
        {
            var posts = await _postService.GetDraftedPosts(this.GetCurrentUserId(), cancellationToken);
            if (posts.Any())
            {
                return Ok(posts);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateModel model, CancellationToken cancellationToken = default)
        {
            await _postService.AddPost(model.Title, model.Text, this.GetCurrentUserId(), model.IsPublishedNow, cancellationToken);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> EditPost([FromRoute] int id, [FromBody] PostUpdateModel model, CancellationToken cancellationToken = default)
        {
            await _postService.UpdatePost(id, this.GetCurrentUserId(), model.NewTitle, model.NewText, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}/publish")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> PublishPost([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _postService.PublishPost(id, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/unpublish")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> UnpublishPost([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _postService.UnpublishPost(id, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> DeletePost([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _postService.DeletePost(id, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }
    }
}
