using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("name")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetName()
        {
            var name = await _userService.GetName(this.GetCurrentUserId());

            return Ok(name);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.CreateUser(model.Email, model.Password, model.Name);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
