//either a user or an error

namespace Semester4_Project.Business.Models;

public class AccountResult
{
    public UserInfo? User { get; set; }
    public string? Error { get; set; }

    public bool Success => Error == null;
}
