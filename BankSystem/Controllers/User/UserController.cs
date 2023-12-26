using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Auth;

namespace Web_Application.Controllers.User;

public partial class UserController : Controller
{
    
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserController> _logger;

    public UserController(
        UserManager<AppUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        ILogger<UserController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    
}