using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Auth;
using Web_Application.Models.Account;

namespace Web_Application.Controllers.Account;

public partial class AccountController 
{
    
    // GET: Register
    // Handles the HTTP GET request for the registration page
    public IActionResult Register()
    {
        _logger.LogInformation("Register GET action called.");
        return View();
    }
    
    // POST: Register
    // Handles the HTTP POST request for user registration
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // Log action entry
        _logger.LogInformation("Register POST action called.");

        // Check if the model state is valid
        if (ModelState.IsValid)
        {
            // Check if the entered password matches the confirmation password
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
                _logger.LogWarning("Password and confirmation password do not match.");
                return View(model);
            }

            // Create a new user with the provided email and username
            var user = new AppUser { UserName = model.Email, Email = model.Email };
            
            // Attempt to create the user using the user manager
            var result = await _userManager.CreateAsync(user, model.Password!);

            // Check if user creation was successful
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Клиент");
                // Registration successful, redirect to the Home page
                _logger.LogInformation($"User {user.UserName} registered successfully.");
                return RedirectToAction("Index", "Home");
            }

            // If there are errors during user creation, add them to the model state
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                _logger.LogError($"Error during user registration: {error.Description}");
            }
        }

        // If the model state is not valid, log a warning
        _logger.LogWarning("Register POST action: ModelState is not valid.");
        return View(model);
    }
}