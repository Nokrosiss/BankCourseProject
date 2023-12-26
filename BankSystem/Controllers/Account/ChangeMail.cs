using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.Account;

namespace Web_Application.Controllers.Account;

public partial class AccountController
{
    
    // GET: ChangeMail
    // Handles the HTTP GET request for the ChangeMail page
    [Authorize]
    [HttpGet]
    public IActionResult ChangeMail()
    {
        // Implement logic to display the ChangeMail view
        _logger.LogInformation("ChangeMail GET action called.");
        return View();
    }

    // POST: ChangeMail
    // Handles the HTTP POST request for changing user email
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeMail(ChangeMailViewModel model)
    {
        // Log action entry
        _logger.LogInformation("ChangeMail POST action called.");

        // Check if the model state is valid
        if (ModelState.IsValid)
        {
            // Retrieve the currently logged-in user from the user manager
            var user = await _userManager.GetUserAsync(User);

            // Check if the user exists
            if (user != null)
            {
                try
                {
                    // Attempt to update the user's email
                    user.Email = model.NewEmail;
                    var result = await _userManager.UpdateAsync(user);

                    // Check if the email update was successful
                    if (result.Succeeded)
                    {
                        // Email change successful, redirect to a success page or another action
                        _logger.LogInformation($"Email change successful for user {user.UserName}.");
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        // If there are errors during email update, add them to the model state
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            _logger.LogError($"Error updating user email: {error.Description}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occurred during email update
                    _logger.LogError($"An error occurred while updating user email: {ex.Message}");
                    // Handle other exception details if needed
                }
            }
            else
            {
                // Log the case where the user cannot be found
                ModelState.AddModelError(string.Empty, "User not found.");
                _logger.LogError("User not found during email change.");
            }
        }

        // If the model state is not valid, return the view with the model
        return View(model);
    }
}