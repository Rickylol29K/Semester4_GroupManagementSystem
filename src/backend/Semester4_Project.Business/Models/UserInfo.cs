//user without the password hash

namespace Semester4_Project.Business.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
