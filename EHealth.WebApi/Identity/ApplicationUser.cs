using Microsoft.AspNetCore.Identity;

namespace EHealth.WebApi.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
