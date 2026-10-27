namespace Semester4_Project.Business;

// The user data the rest of the app may see (no password hash).
public class UserInfo
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
