using Microsoft.AspNetCore.Identity;

namespace EHealth.Identity
{
    public class ApplicationUser : IdentityUser, IAuthorizable
    {
        public string FullName { get; set; }
    }
}
