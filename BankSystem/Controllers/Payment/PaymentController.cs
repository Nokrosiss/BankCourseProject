using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Auth;
using Web_Application.Data;

namespace Web_Application.Controllers.Payment
{
    public partial class PaymentController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<PaymentController> _logger;
        private readonly CustomDbContext _dbContext;

        public PaymentController(
            UserManager<AppUser> userManager,
            ILogger<PaymentController> logger,
            CustomDbContext dbContext
        ) 
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}