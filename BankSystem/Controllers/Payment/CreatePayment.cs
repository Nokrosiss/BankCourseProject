using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Application.Models.Payment;

namespace Web_Application.Controllers.Payment
{
    public partial class PaymentController
    {
        [HttpGet]
        public async Task<IActionResult> CreatePayment()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var userCredits = _dbContext.Credits
                .Where(c => c.UserId == user.Id)
                .ToList();

            var creditList = userCredits.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"CreditId: {c.Id} - Amount: {c.Amount}"
            }).ToList();

            ViewBag.CreditList = creditList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return Unauthorized();
                }

                var selectedCredit = _dbContext.Credits.FirstOrDefault(c => c.Id == model.CreditId);

                if (selectedCredit != null)
                {
                    // Subtract payment amount from credit amount
                    selectedCredit.Amount -= model.Amount;

                    // Check if credit amount is 0 or less
                    if (selectedCredit.Amount <= 0)
                    {
                        // Change status to Closed and set amount to 0
                        selectedCredit.Status = "Closed";
                        selectedCredit.Amount = 0;
                    }

                    // Map the ViewModel to the Payment entity
                    var payment = new Data.Entities.Payment
                    {
                        UserId = user.Id,
                        CreditId = model.CreditId,
                        Amount = model.Amount,
                        Date = DateTime.UtcNow,
                    };

                    // Add the payment to the database
                    _dbContext.Payments.Add(payment);
                    await _dbContext.SaveChangesAsync();

                    // Redirect to the payment index page
                    return RedirectToAction("PaymentsHistory");
                }
                else
                {
                    // Handle case where selected credit is not found
                    return NotFound("Selected credit not found.");
                }
            }

            return View(model);
        }

    }
}
