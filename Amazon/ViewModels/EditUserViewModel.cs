using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Amazon.ViewModels
{
    public class EditUserViewModel : IdentityRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SelectedRole { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public bool IsEnabled { get; set; }
        public string Status { get; set; }
    }
}
