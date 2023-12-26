using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.User;

namespace Web_Application.Controllers.User;

public partial class UserController
{
    
    [HttpPost]
    public IActionResult UpdateUser(ModifyUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // If the model state is not valid, return the view with validation errors
            return View("ModifyUser", model);
        }

        // Fetch the user based on the model's UserId
        var user = _userManager.FindByIdAsync(model.UserId).Result;

        if (user == null)
        {
            // Handle user not found
            return NotFound();
        }

        // Update user details
        user.UserName = model.UserName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;

        var result = _userManager.UpdateAsync(user).Result;
        if (result.Succeeded)
        {
            // User details updated successfully
            // Redirect to the ManageUsers view or perform any necessary actions
            return RedirectToAction("ModifyUser", new { userId = model.UserId });
        }

        // Handle the case where user update fails
        // You can redirect to an error page or perform other error handling
        return NotFound();
    }
}