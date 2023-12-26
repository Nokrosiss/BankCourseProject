using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Auth;
using Web_Application.Data;

namespace Web_Application.Controllers.Account;

public partial class AccountController : Controller
{
    
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<AccountController> _logger;


    public AccountController(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        ILogger<AccountController> logger
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
 
    }
    
}
