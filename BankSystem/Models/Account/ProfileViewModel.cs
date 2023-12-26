using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models.Account;

public class ProfileViewModel
{
    [Display(Name = "Username")]
    public string? UserName { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Full Name")]
    public string? FullName { get; set; }

    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Profile Picture")]
    public string? ProfilePictureUrl { get; set; }
}
