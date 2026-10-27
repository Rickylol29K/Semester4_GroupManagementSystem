namespace Semester4_Project.Business;

// Either a user (success) or an error message (failure).
public class AccountResult
{
    public UserInfo? User { get; set; }
    public string? Error { get; set; }

    public bool Success => Error == null;
}
