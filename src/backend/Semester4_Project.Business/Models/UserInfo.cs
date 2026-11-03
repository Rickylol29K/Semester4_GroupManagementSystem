// The user data the rest of the app may see. The password hash is left out on purpose.

namespace Semester4_Project.Business.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
