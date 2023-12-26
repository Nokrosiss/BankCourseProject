using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.Account;

namespace Web_Application.Controllers.Account;

public partial class AccountController
{
    
     // GET: ChangePassword
    // Handles the HTTP GET request for the ChangePassword page
    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        // Implement logic to display the ChangePassword view
        _logger.LogInformation("ChangePassword GET action called.");
        return View();
    }
    
    // POST: ChangePassword
    // Handles the HTTP POST request for changing user password
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        // Log action entry
        _logger.LogInformation("ChangePassword POST action called.");

        // Check if the model state is valid
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ChangePassword: ModelState is not valid.");
            return View(model);
        }

        // Retrieve the currently logged-in user from the user manager
        var user = await _userManager.GetUserAsync(User);

        // Check if the user exists
        if (user == null)
        {
            // Log and redirect to the login page if the user is not found
            _logger.LogWarning("ChangePassword: User not found. Redirecting to Login.");
            return RedirectToAction("Login");
        }

        try
        {
            // Attempt to change the user's password
            var changePasswordResult = await _userManager.ChangePasswordAsync(
                user, 
                model.OldPassword!, 
                newPassword:model.NewPassword!
            );

            // Check if the password change was successful
            if (changePasswordResult.Succeeded)
            {
                // Password changed successfully, you can redirect to a success page or perform other actions
                _logger.LogInformation($"Password changed successfully for user {user.UserName}.");
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                // If there are errors during password change, add them to the model state
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogError($"Error changing password for user {user.UserName}: {error.Description}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occurred during password change
            _logger.LogError($"An error occurred while changing password: {ex.Message}");
            // Handle other exception details if needed
        }

        // If the password change was not successful or an exception occurred, return the view with the model
        return View(model);
    }
}