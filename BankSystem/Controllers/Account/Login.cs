using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.Account;

namespace Web_Application.Controllers.Account;

public partial class AccountController
{
    
    // GET: Login
    // Handles the HTTP GET request for the login page
    public IActionResult Login()
    {
        _logger.LogInformation("Login GET action called.");
        return View();
    }
    
    // POST: Login
    // Handles the HTTP POST request for user login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        // Log action entry
        _logger.LogInformation("Login POST action called.");

        // Check if the model state is valid
        if (ModelState.IsValid)
        {
            // Attempt to sign in the user using the provided credentials
            var result = await _signInManager.PasswordSignInAsync(
                viewModel.Email!, 
                viewModel.Password!, 
                viewModel.RememberMe, 
                lockoutOnFailure: false
            );

            // Check if the login attempt was successful
            if (result.Succeeded)
            {
                // User can log in, perform any desired actions
                // For example, set authentication cookies and redirect
                // to a specific page or the returnUrl

                // For example, redirect to the Home page
                _logger.LogInformation($"User {viewModel.Email} logged in successfully.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // User cannot log in, display an error message and stay on the login page
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                _logger.LogWarning($"Invalid login attempt for user {viewModel.Email}.");
            }
        }

        // If the model state is not valid, log a warning
        _logger.LogWarning("Login POST action: ModelState is not valid.");
        return View(viewModel);
    }
}