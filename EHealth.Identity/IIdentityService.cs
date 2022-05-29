using System;
using System.Threading.Tasks;

namespace EHealth.Identity
{
    public interface IIdentityService<out TUser> where TUser : class, IAuthorizable
    {
        Task<bool> EnsureRoleExistsAsync(string role);
        Task<string> GenerateTokenAsync(string userName);
        Task<string> GetFullName(string userName);
        Task<bool> LoginAsync(string userName, string password);
        Task<bool> RegisterAsync(Action<TUser> setRegistration, string password, string role);
    }
}
