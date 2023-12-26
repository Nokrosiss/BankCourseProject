using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Auth;
using Web_Application.Controllers.Account;
using Web_Application.Data;

namespace Web_Application.Controllers.Credit
{
    public partial class CreditController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CreditController> _logger;
        private readonly CustomDbContext _dbContext;

        public CreditController(
            UserManager<AppUser> userManager,
            ILogger<CreditController> logger,
            CustomDbContext dbContext
            ) 
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
