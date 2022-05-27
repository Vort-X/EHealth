using EHealth.Identity;
using EHealth.Identity.Default;
using EHealth.WebApi.ViewModel.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EHealth.WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService<ApplicationUser> identityService;

        public AuthenticationController(IIdentityService<ApplicationUser> identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var (loggedIn, user) = await identityService.TryLoginAsync(model.UserName, model.Password);
            if (!loggedIn)
            {
                return Ok(new { error = "Login failed: Incorrect login or password" });
            }

            var token = await identityService.GenerateTokenAsync(user);
            return Ok(new { token });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = await identityService.GenerateUserAsync();
            user.UserName = model.UserName;
            user.FullName = model.FullName;
            var userAdded = await identityService.CreateUserAsync(user, model.Password);

            return userAdded ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
