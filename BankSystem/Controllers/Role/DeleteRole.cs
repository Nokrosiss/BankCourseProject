using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Role;

public partial class RoleController 
{
    
    [HttpPost]
    public async Task<IActionResult> DeleteRole(string roleId)
    {
        try
        {
            // Find the role by its ID
            var role = await _roleManager.FindByIdAsync(roleId);

            // If role not found, log a warning and return NotFound result
            if (role == null)
            {
                _logger.LogWarning($"Role with ID '{roleId}' not found.");
                return NotFound("Role not exists");
            }

            // Attempt to delete the role
            var result = await _roleManager.DeleteAsync(role);

            // If deletion succeeded, log success and redirect to ManageRoles action
            if (result.Succeeded)
            {
                _logger.LogInformation($"Role '{role.Name}' deleted successfully.");
                return RedirectToAction("ManageRoles");
            }

            // If deletion failed, log errors, add error to ModelState, and return ManageRoles view
            _logger.LogError($"Failed to delete role '{role.Name}'. Errors: {string.Join(", ", result.Errors)}");
            ModelState.AddModelError("", "Failed to delete the role. Please try again.");
            return View("ManageRoles"); // Replace "ManageRoles" with your actual view name for role management
        }
        catch (Exception ex)
        {
            // If an exception occurs, log the error and return StatusCode 500
            _logger.LogError(ex, "An error occurred while deleting the role.");
            return StatusCode(500, "An error occurred while deleting the role.");
        }
    }
}