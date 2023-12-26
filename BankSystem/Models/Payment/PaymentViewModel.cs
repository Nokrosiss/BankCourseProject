namespace Web_Application.Models.Payment;
using System;
using System.ComponentModel.DataAnnotations;

public class PaymentViewModel
{
    [Required]
    [Display(Name = "Credit")]
    public int CreditId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid amount.")]
    public decimal Amount { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Payment Date")]
    public DateTime Date { get; set; }
}
