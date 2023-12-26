using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Role;

public partial class RoleController
{
    
    [HttpPost]
    public async Task<IActionResult> UpdateRole(string roleId, string newRoleName)
    {
        try
        {
            // Find the role by its ID
            var role = await _roleManager.FindByIdAsync(roleId);

            // If role not found, log a warning and return NotFound result
            if (role == null)
            {
                _logger.LogWarning($"Role with ID '{roleId}' not found.");
                return NotFound();
            }

            // Update the role name
            role.Name = newRoleName;

            // Attempt to update the role
            var result = await _roleManager.UpdateAsync(role);

            // If update succeeded, log success and redirect to ManageRoles action
            if (result.Succeeded)
            {
                _logger.LogInformation($"Role '{role.Name}' updated successfully.");
                return RedirectToAction("ManageRoles");
            }

            // If update failed, log errors, add error to ModelState, and return ManageRoles view
            _logger.LogError($"Failed to update role '{role.Name}'. Errors: {string.Join(", ", result.Errors)}");
            ModelState.AddModelError("", "Failed to update the role. Please check the provided information.");
            return View("ManageRoles");
        }
        catch (Exception ex)
        {
            // If an exception occurs, log the error and return StatusCode 500
            _logger.LogError(ex, "An error occurred while updating the role.");
            return StatusCode(500, "An error occurred while updating the role.");
        }
    }
}