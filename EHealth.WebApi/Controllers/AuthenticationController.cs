using EHealth.WebApi.Identity;
using EHealth.WebApi.ViewModel.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        static bool isSeeded;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;

            Seed().Wait();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            bool hasValidPassword = await userManager.CheckPasswordAsync(user, model.Password);
            if (user is null || !hasValidPassword)
            {
                return Unauthorized();
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(ur => new Claim(ClaimTypes.Role, ur)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddMinutes(120),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new 
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var existingUser = await userManager.FindByNameAsync(model.UserName);
            if (existingUser is not null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var user = new ApplicationUser()
            {
                FullName = model.FullName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var registerResult = await userManager.CreateAsync(user, model.Password);
            if (!registerResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var roleResult = await userManager.AddToRoleAsync(user, UserRoles.User);
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        async Task Seed()
        {
            if (isSeeded) return;

            await InsertRole(UserRoles.Admin);
            await InsertRole(UserRoles.User);

            isSeeded = true;

            async Task InsertRole(string roleName)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
