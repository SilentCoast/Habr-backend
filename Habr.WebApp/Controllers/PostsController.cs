using Habr.Services;
using Habr.WebApp.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> GetDraftedPosts([FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            var posts = await _postService.GetDraftedPosts(userId, cancellationToken);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> CreatePost([FromHeader] int userId, [FromBody] PostCreateModel model, CancellationToken cancellationToken = default)
        {
            await _postService.AddPost(model.Title, model.Text, userId, model.IsPublishedNow, cancellationToken);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> EditPost([FromRoute] int id, [FromBody] PostUpdateModel model, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            await _postService.UpdatePost(id, userId, model.NewTitle, model.NewText, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> PublishPost([FromRoute] int id, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            await _postService.PublishPost(id, userId, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/unpublish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> UnpublishPost([FromRoute] int id, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            await _postService.UnpublishPost(id, userId, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> DeletePost([FromRoute] int id, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            await _postService.DeletePost(id, userId, cancellationToken);
            return Ok();
        }
    }
}
