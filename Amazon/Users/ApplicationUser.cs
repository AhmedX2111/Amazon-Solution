using Microsoft.AspNetCore.Identity;

namespace Amazon.Users
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; } = true; // Default to true (enabled)
    }
}
