using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amazon.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmail(string email)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                // Check if the user is an Admin
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("AdminOnlyAction");
                }
                else
                {
                    return RedirectToAction("UserAction"); // Assuming you have a UserAction for non-admin users
                }
            }

            // If user not found, return to an error view or handle as needed
            ModelState.AddModelError("", "User not found");
            return View("Error"); // Replace with your error view
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyAction()
        {
            return View(); // Return admin view
        }

        public IActionResult UserAction()
        {
            return View(); // Return user view
        }
    }
}
