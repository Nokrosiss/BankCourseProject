using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models.Account;

public class ChangeMailViewModel
{
    [Required(ErrorMessage = "Current Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? CurrentEmail { get; set; }

    [Required(ErrorMessage = "New Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? NewEmail { get; set; }

    [Compare("NewEmail", ErrorMessage = "Emails do not match")]
    public string? ConfirmNewEmail { get; set; }
}
