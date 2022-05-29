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

        public async Task<bool> RegisterAsync(Action<ApplicationUser> setRegistration, string password, string role)
        {
            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString()
            };
            setRegistration(user);
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

            var roleExists = await EnsureRoleExistsAsync(role);
            if (!roleExists)
            {
                await userManager.DeleteAsync(user);
                return false;
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, role);
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

            var createResult = await roleManager.CreateAsync(new(role));
            return createResult.Succeeded;
        }

        public async Task<string> GenerateTokenAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return string.Empty;
            }

            var authClaims = (await userManager.GetRolesAsync(user))
                .Select(r => new Claim(ClaimTypes.Role, r))
                .Prepend(new Claim(ClaimTypes.Name, userName))
                .Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

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

        public async Task<string> GetFullName(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user.FullName;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user is not null && await userManager.CheckPasswordAsync(user, password);
        }

        //TODO: Make another component for seeding
        async Task Seed()
        {
            if (isSeeded) return;

            await EnsureRoleExistsAsync(UserRoles.Admin);
            await EnsureRoleExistsAsync(UserRoles.Doctor);
            await EnsureRoleExistsAsync(UserRoles.User);

            isSeeded = true;
        }
    }
}
