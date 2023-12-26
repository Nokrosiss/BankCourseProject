using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Models;
using Web_Application.Models.Account;

namespace Web_Application.Controllers.Account;

public partial class AccountController
{
    
    // GET: Profile
    // Handles the HTTP GET request for the user profile page
    [Authorize]
    public IActionResult Profile()
    {
        // Log action entry
        _logger.LogInformation("Profile GET action called.");

        // Retrieve the currently logged-in user from the user manager
        var user = _userManager.GetUserAsync(User).Result;

        // Check if the user exists
        if (user != null)
        {
            // Create a view model to represent the user profile
            var profileViewModel = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                // Populate other profile properties as needed
            };

            // Log the successful retrieval of the user profile
            _logger.LogInformation($"User profile retrieved successfully for user {user.UserName}.");

            // Return the user profile view
            return View(profileViewModel);
        }
        else
        {
            // Log the case where the user cannot be found
            _logger.LogError("User not found in Profile action.");
            // Optionally, handle the case where the user cannot be found, for example, redirecting to an error page.
            return RedirectToAction("Error", "Home");
        }
    }
}