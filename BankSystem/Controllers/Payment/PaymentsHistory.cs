using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Application.Controllers.Payment;

public partial class PaymentController
{
    
    public async Task<IActionResult> PaymentsHistory()
    {
        // Get the current authenticated user
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            // Handle the case where the user is not authenticated
            return Unauthorized();
        }

        // Retrieve payments for the current user
        var payments = await _dbContext.Payments
            .Where(p => p.UserId == user.Id)
            .ToListAsync();

        return View(payments);
    }
}