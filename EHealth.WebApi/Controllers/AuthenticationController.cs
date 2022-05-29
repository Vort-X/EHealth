using EHealth.Identity;
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
        private readonly IIdentityService<IAuthorizable> identityService;

        public AuthenticationController(IIdentityService<IAuthorizable> identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var loggedIn = await identityService.LoginAsync(model.UserName, model.Password);
            if (!loggedIn)
            {
                return Ok(new { error = "Login failed: Incorrect login or password" });
            }
            var token = await identityService.GenerateTokenAsync(model.UserName);

            return Ok(new { token });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var userAdded = await identityService.RegisterAsync(user => 
            {
                user.UserName = model.UserName;
                user.FullName = model.FullName;
            }, 
            model.Password, UserRoles.User);

            return userAdded ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
