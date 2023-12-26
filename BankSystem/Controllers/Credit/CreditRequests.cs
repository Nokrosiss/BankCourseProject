using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Credit;

public partial class CreditController
{
    public IActionResult CreditRequests()
    {
        try
        {
            // Retrieve credit history for all users
            var creditHistory = _dbContext.Credits.ToList();
            return View(creditHistory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving credit history.");
            // Return an error response or handle as appropriate
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }
}