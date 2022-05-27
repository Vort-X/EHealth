using Microsoft.AspNetCore.Identity;

namespace EHealth.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
