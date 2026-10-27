// One row in the Users table: an account with an email, a hashed password and a role.

namespace Semester4_Project.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";

    // "Student" or "Teacher"
    public string Role { get; set; } = "";
}
