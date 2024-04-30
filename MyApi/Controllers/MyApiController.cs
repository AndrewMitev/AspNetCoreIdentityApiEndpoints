using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;

        public MyApiController(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost("assign/athlete")]
        public async Task<IActionResult> RegisterAsAthlete()
        {
            var user = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this.userManager.FindByEmailAsync(user);

            if (loggedInUser is null)
            {
                return NotFound();
            }

            IdentityResult result = await this.userManager.AddToRoleAsync(loggedInUser, RoleConstants.Athlete);

            if (result.Succeeded) 
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
