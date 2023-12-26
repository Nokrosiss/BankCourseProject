using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.User;

namespace Web_Application.Controllers.User;

public partial class UserController
{
    public IActionResult ModifyUser(string userId)
    {
        var user = _userManager.FindByIdAsync(userId).Result;

        if (user == null)
        {
            // Handle user not found
            return NotFound();
        }

        // Retrieve user roles
        var userRoles = _userManager.GetRolesAsync(user).Result;
        foreach (var roleName in userRoles)
        {
            Console.WriteLine(roleName);
        }

        var availableRoles = _roleManager.Roles.ToList();

        // Pass user data and roles to the ModifyUser view
        var model = new ModifyUserViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Roles = userRoles,
            AvailableRoles = availableRoles
        };

        return View(model);
    }
}