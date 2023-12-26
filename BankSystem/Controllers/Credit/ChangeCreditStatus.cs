using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers.Credit;

public partial class CreditController
{
    
    [HttpPost]
    public IActionResult ChangeCreditStatus(int creditId, string newStatus)
    {
        try
        {
            // Retrieve the credit from the database based on the creditId
            var credit = _dbContext.Credits.Find(creditId);

            if (credit == null)
            {
                // Handle the case where the credit is not found
                return NotFound();
            }

            // Update the credit status
            credit.Status = newStatus;

            // Save changes to the database
            _dbContext.SaveChanges();

            // Redirect back to the credit history page
            return RedirectToAction("CreditHistory");
        }
        catch (Exception ex)
        {
            // Log the error
            _logger.LogError(ex, "Error while changing credit status.");

            // Handle the error gracefully, you might want to show an error page or redirect to a specific view
            return View("Error");
        }
    }
    
}