using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.User;

namespace Web_Application.Controllers.User
{
    public partial class UserController
    {
        [HttpPost]
        public async Task<IActionResult> AddUserRole(ModifyUserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    // Handle user not found
                    var userErrorMessage = $"User not found with ID: {model.UserId}";
                    _logger.LogWarning(userErrorMessage);
                    return NotFound(userErrorMessage);
                }

                var roleExists = await _roleManager.RoleExistsAsync(model.SelectedRole);

                if (!roleExists)
                {
                    // Handle role not found
                    ModelState.AddModelError("", "Role does not exist.");
                    return View("ModifyUser", model); // Redirect to ModifyUser view with the model
                }

                var result = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (result.Succeeded)
                {
                    // Role added successfully

                    // Reload user roles after adding a new role
                    var updatedUserRoles = await _userManager.GetRolesAsync(user);

                    // Update the model with the new roles
                    model.Roles = updatedUserRoles;

                    var successMessage = $"Role '{model.SelectedRole}' added to user '{user.UserName}' successfully.";
                    _logger.LogInformation(successMessage);

                    // Redirect to the ModifyUser view with the complete userId parameter
                    return RedirectToAction("ModifyUser", new { userId = model.UserId });
                }

                // Handle error adding role
                var addRoleErrorMessage = $"Failed to add role '{model.SelectedRole}' to user '{user.UserName}'. Errors: {string.Join(", ", result.Errors)}";
                _logger.LogError(addRoleErrorMessage);
                ModelState.AddModelError("", "Failed to add role.");
            }

            // Handle invalid role selection or other issues
            return View("ModifyUser", model); // Redirect to ModifyUser view with the model
        }
    }
}
