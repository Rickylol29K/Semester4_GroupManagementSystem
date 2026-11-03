//Users table

namespace Semester4_Project.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";

    //Student or Teacher
    public string Role { get; set; } = "";
}
