using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Models.Credit;

namespace Web_Application.Controllers.Credit
{
    public partial class CreditController
    {

        public IActionResult CreateCredit()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> CreateCredit(CreditViewModel model)
        {
            try
            {
                // Get the current user
                var user = await _userManager.GetUserAsync(User);

                Console.WriteLine($"User: {user?.UserName} ({user?.Id})");

                if (user == null)
                {
                    // User not found, handle accordingly (e.g., return a view with an error message)
                    Console.WriteLine("User not found.");
                    ViewBag.ErrorMessage = "User not found.";
                    return View("Error");
                }

                // Custom validation checks
                Console.WriteLine($"Amount: {model.Amount}");

                string amountValidationErrorMessage = GetAmountValidationErrorMessage(model.Amount);
                if (!string.IsNullOrEmpty(amountValidationErrorMessage))
                {
                    // Amount validation failed
                    Console.WriteLine($"Amount validation failed: {amountValidationErrorMessage}");
                    ViewBag.ErrorMessage = amountValidationErrorMessage;
                    return View("Error");
                }

                Console.WriteLine($"Passport: {model.Passport}");

                if (string.IsNullOrWhiteSpace(model.Passport))
                {
                    // Passport number is required
                    Console.WriteLine("Passport number is required.");
                    ViewBag.ErrorMessage = "Passport number is required.";
                    return View("Error");
                }
                

                // Add more custom validation checks as needed

                // If any validation errors, handle accordingly
                if (ValidationFailed(model))
                {
                    // Set status to Rejected if validation fails
                    Console.WriteLine("Validation failed. Credit application rejected.");
                    ViewBag.ErrorMessage = "Credit application rejected due to validation errors.";
                    return View("Error");
                }

                // Map the ViewModel to the Entity
                var credit = new Data.Entities.Credit
                {
                    StartingAmount = model.Amount,
                    Amount = model.Amount + model.Amount/10,
                    UserId = user.Id,
                    DateOfCreation = DateTime.UtcNow,
                    Status = StatusMap[CreditStatus.Issued]
                };

                _dbContext.Credits.Add(credit);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine("Credit application processed successfully.");

                // Return a success view or additional data if needed
                return RedirectToAction("CreditHistory");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing credit application: {ex.Message}");
                _logger.LogError(ex, "Error while processing credit application.");
                ViewBag.ErrorMessage = "Internal server error";
                return View("Error");
            }
        }

        private bool ValidationFailed(CreditViewModel model)
        {
            // Implement your custom validation logic
            // Return true if validation fails, otherwise return false

            // Example: Check if Passport has the required format
            if (!IsRussianPassportValid(model.Passport))
            {
                Console.WriteLine("Passport validation failed.");
                return true;
            }

            // Add more custom validation checks as needed

            return false;
        }
        private bool IsAmountValid(decimal amount)
        {
            // Implement your specific amount validation logic
            // For example, check if the amount is positive and within the range of 10000 to 10000000
            return amount > 0 && amount >= 10000 && amount <= 10000000;
        }

        private bool IsRussianPassportValid(string passport)
        {
            // Implement your specific passport validation logic
            // For example, check if the passport has the format "0000 111111"
            return Regex.IsMatch(passport, @"^\d{4}\s\d{6}$");
        }
        private string GetAmountValidationErrorMessage(decimal amount)
        {
            if (amount <= 0)
            {
                return "Amount must be a positive value.";
            }
            else if (amount < 10000)
            {
                return "Amount must be at least 10000.";
            }
            else if (amount > 10000000)
            {
                return "Amount must not exceed 10000000.";
            }

            // Return null if the amount is valid
            return null;
        }
        
    }
}