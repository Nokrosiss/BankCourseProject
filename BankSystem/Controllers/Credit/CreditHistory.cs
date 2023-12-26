using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Application.Controllers.Credit
{
    public partial class CreditController
    {
        
        
        [HttpGet("CreditHistory")]
        public async Task<IActionResult> CreditHistory()
        {
            try
            {
                // Get the current user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    // User not found, handle accordingly (e.g., return 401 Unauthorized)
                    return Unauthorized(new { Message = "User not found." });
                }

                // Retrieve credit history for the user
                var creditHistory = _dbContext.Credits
                    .Where(c => c.UserId == user.Id)
                    .OrderByDescending(c => c.DateOfCreation)
                    .ToList();

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
}
