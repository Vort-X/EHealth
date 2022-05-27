using Microsoft.AspNetCore.Identity;

namespace EHealth.Identity.Default
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
