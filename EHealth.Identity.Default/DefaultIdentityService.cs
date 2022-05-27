using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.Identity.Default
{
    public class DefaultIdentityService : IIdentityService<ApplicationUser>
    {
        static bool isSeeded;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public DefaultIdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;

            Seed().Wait();
        }

        private const string defaultRole = UserRoles.User;

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var existingUser = await userManager.FindByNameAsync(user.UserName);
            if (existingUser is not null)
            {
                return false;
            }

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return false;
            }

            var roleExists = await EnsureRoleExistsAsync(defaultRole);
            if (!roleExists)
            {
                await userManager.DeleteAsync(user);
                return false;
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, defaultRole);
            if (!addToRoleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                return false;
            }

            return true;
        }

        public async Task<bool> EnsureRoleExistsAsync(string role)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (roleExists) return true;

            var createResult = await roleManager.CreateAsync(new(defaultRole));
            return createResult.Succeeded;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var authClaims = (await userManager.GetRolesAsync(user))
                .Select(async ur => await roleManager.FindByNameAsync(ur))
                .Select(async ir => await roleManager.GetClaimsAsync(await ir))
                .SelectMany(cl => cl.Result)
                .Union(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });

            //TODO: Inject ISecurityKeyProvider in ISecurityTokenProvider
            //Expected:
            //var authSigningKey = securityKeyProvider.GetKey(configuration["JWT:Secret"]);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            //TODO: Inject ISecurityTokenProvider
            //Expected:
            //var token = securityTokenProvider.GenerateToken(authClaims);
            var period = int.TryParse(configuration["JWT:TimeToLive"], out int @unchecked) ? @unchecked : 120;
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddMinutes(period),
                claims: authClaims,
                signingCredentials: new(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            //TODO: Inject ISecurityTokenHandler
            //Expected:
            //return securityTokenHandler.WriteToken(token);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplicationUser> GenerateUserAsync()
        {
            await Task.CompletedTask;
            return new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }

        public async Task<(bool, ApplicationUser)> TryLoginAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user is null) return (false, null);

            var checkPassword = await userManager.CheckPasswordAsync(user, password);
            return (checkPassword, user);
        }

        //TODO: Make another component for seeding
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
