using Amazon.Users;
using Amazon.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: /Admin/Index
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            var model = new AdminViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault(); // Get the first role if there are multiple
            var roles = await _roleManager.Roles.ToListAsync(); // Ensure roles have the Name property

            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                SelectedRole = currentRole, // Current role
                
                Roles = roles // Ensure roles list is populated correctly

            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId) as ApplicationUser;
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.UserName = model.UserName;
            user.Email = model.Email;

            // Update user roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to remove existing roles.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                var addRoleResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (!addRoleResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to add the new role.");
                    return View(model);
                }
            }

            // Update user's enabled status
            user.IsEnabled = model.IsEnabled;

            // Disable user login if IsEnabled is false
            if (!user.IsEnabled)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;
            }
            else
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Redirect to the Admin page
                return RedirectToAction("Index", "Admin");
            }

            // Handle errors during the update
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Repopulate roles in case of an error
            model.Roles = _roleManager.Roles.ToList();

            return View(model);
        }


        public async Task<IActionResult> ManageRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }







    }
}
