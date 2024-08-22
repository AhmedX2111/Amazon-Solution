using Microsoft.AspNetCore.Identity;

namespace Amazon.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
