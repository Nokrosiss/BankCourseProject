using Microsoft.AspNetCore.Identity;

namespace Web_Application.Models.User;

public class ModifyUserViewModel
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public  string? PhoneNumber { get; set; }
    public string? SelectedRole { get; set;  }
    
    public IList<IdentityRole>? AvailableRoles { get; set; }
    public IList<string>? Roles { get; set; }
}