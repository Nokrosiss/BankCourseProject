using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Application.Models.User;

namespace Web_Application.Controllers.User;

public partial class UserController
{
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            // Redirect to ManageUsers after successful deletion
            return RedirectToAction("ManageUsers");
        }

        // Handle errors if the deletion fails
        ModelState.AddModelError("", "Failed to delete the user.");
        return View("ManageUsers");
    }
}