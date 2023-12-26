using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.Role;

namespace Web_Application.Controllers.Role;

public partial class RoleController
{
    public IActionResult ManageRoles()
    {
        // Retrieve roles from your data source (Identity Framework using RoleManager)
        var roles = _roleManager.Roles.ToList();

        // Map roles to RoleViewModels
        var roleViewModels = roles.Select(role => new RoleViewModel
        {
            Id = role.Id,
            RoleName = role.Name
        });

        // Pass the collection of RoleViewModels to the view for rendering
        return View(roleViewModels);
    }
}