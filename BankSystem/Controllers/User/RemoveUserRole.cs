using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.User;

public partial class UserController
{
    
    [HttpPost]
    public IActionResult RemoveUserRole(string userId, string roleName)
    {
        // Fetch the user based on the userId
        var user = _userManager.FindByIdAsync(userId).Result;

        if (user == null)
        {
            // Handle user not found
            return NotFound();
        }

        // Check if the user has the role
        var isInRole = _userManager.IsInRoleAsync(user, roleName).Result;
        if (!isInRole)
        {
            // Handle the case where the user doesn't have the role
            return NotFound();
        }

        // Remove the role from the user
        var result = _userManager.RemoveFromRoleAsync(user, roleName).Result;
        if (result.Succeeded)
        {
            // Role removed successfully
            // Redirect to the modify user view or perform any necessary actions
            return RedirectToAction("ModifyUser", new { userId = userId });
        }

        // Handle the case where role removal fails
        // You can redirect to an error page or perform other error handling
        return View("ModifyUser");
    }
}