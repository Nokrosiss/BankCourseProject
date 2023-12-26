using Web_Application.Auth;

namespace Web_Application.Models.User;

public class ManageUsersViewModel
{
    public List<AppUser>? Users { get; set; }
    public Dictionary<string, List<string>>? UserRoles { get; set; }
}
