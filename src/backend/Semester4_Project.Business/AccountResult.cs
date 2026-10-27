// The answer AccountService gives back: either a User (it worked) or an Error message (it failed).

namespace Semester4_Project.Business;

public class AccountResult
{
    public UserInfo? User { get; set; }
    public string? Error { get; set; }

    public bool Success => Error == null;
}
