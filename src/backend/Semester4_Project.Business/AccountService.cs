// The rules for accounts: registering, logging in and listing students.
// Checks things like "is this email already used?" and "is the password correct?".

using Microsoft.AspNetCore.Identity;
using Semester4_Project.DataAccess.Entities;
using Semester4_Project.DataAccess.Repositories;

namespace Semester4_Project.Business;

public class AccountService
{
    private readonly UserRepository _users;

    // Turns a password into a salted hash, so we never store the real password.
    private readonly PasswordHasher<User> _hasher = new();

    public AccountService(UserRepository users)
    {
        _users = users;
    }

    public async Task<AccountResult> RegisterAsync(string email, string password, string role)
    {
        email = email.Trim().ToLower();

        if (!Roles.IsValid(role))
        {
            return new AccountResult { Error = "Role must be Student or Teacher." };
        }

        if (await _users.EmailExistsAsync(email))
        {
            return new AccountResult { Error = "An account with this email already exists." };
        }

        var user = new User { Email = email, Role = role };
        user.PasswordHash = _hasher.HashPassword(user, password);
        await _users.AddAsync(user);

        return new AccountResult { User = ToUserInfo(user) };
    }

    public async Task<AccountResult> LoginAsync(string email, string password)
    {
        var user = await _users.GetByEmailAsync(email.Trim().ToLower());

        // Same message for both cases, so nobody can find out which emails exist.
        if (user == null || _hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
        {
            return new AccountResult { Error = "Email or password is incorrect." };
        }

        return new AccountResult { User = ToUserInfo(user) };
    }

    public async Task<List<UserInfo>> GetStudentsAsync()
    {
        var students = await _users.GetByRoleAsync(Roles.Student);
        return students.Select(ToUserInfo).ToList();
    }

    private static UserInfo ToUserInfo(User user)
    {
        return new UserInfo { Id = user.Id, Email = user.Email, Role = user.Role };
    }
}
