using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Role;

public partial class RoleController 
{
    
    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        try
        {
            // Create a new role object with the provided roleName
            var newRole = new IdentityRole { Name = roleName };

            // Attempt to create the new role
            var result = await _roleManager.CreateAsync(newRole);

            // If creation succeeded, log success and redirect to ManageRoles action
            if (result.Succeeded)
            {
                _logger.LogInformation($"Role '{newRole.Name}' created successfully.");
                return RedirectToAction("ManageRoles");
            }

            // If creation failed, log errors, add error to ModelState, and return ManageRoles view
            _logger.LogError($"Failed to create role '{newRole.Name}'. Errors: {string.Join(", ", result.Errors)}");
            ModelState.AddModelError("", "Failed to create the role. Please check the provided information.");
            return View("ManageRoles");
        }
        catch (Exception ex)
        {
            // If an exception occurs, log the error and return StatusCode 500
            _logger.LogError(ex, "An error occurred while creating the role.");
            return StatusCode(500, "An error occurred while creating the role.");
        }
    }
}