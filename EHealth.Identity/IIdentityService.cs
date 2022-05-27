using System;
using System.Threading.Tasks;

namespace EHealth.Identity
{
    public interface IIdentityService<TUser> where TUser : class
    {
        Task<bool> CreateUserAsync(TUser user, string password);
        Task<bool> EnsureRoleExistsAsync(string role);
        Task<string> GenerateTokenAsync(TUser user);
        Task<TUser> GenerateUserAsync();
        Task<(bool, TUser)> TryLoginAsync(string userName, string password);
    }
}
