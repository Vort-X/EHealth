using EHealth.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class StubIdentityService : IIdentityService<IAuthorizable>
    {
        public async Task<bool> EnsureRoleExistsAsync(string role)
        {
            return await Task.FromResult(true);
        }

        public async Task<string> GenerateTokenAsync(string userName)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<string> GetFullName(string userName)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> RegisterAsync(Action<IAuthorizable> setRegistration, string password)
        {
            return await Task.FromResult(true);
        }
    }
}
