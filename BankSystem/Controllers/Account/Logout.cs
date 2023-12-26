using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Account;

public partial class AccountController
{
    // POST: Logout
    // Handles the HTTP POST request for user logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        // Log the logout attempt
        _logger.LogInformation("Logout POST action called.");

        // Sign out the currently authenticated user
        await _signInManager.SignOutAsync();

        // Redirect to the Home page or another page after logout
        _logger.LogInformation("User logged out successfully.");
        return RedirectToAction("Index", "Home");
    }
}