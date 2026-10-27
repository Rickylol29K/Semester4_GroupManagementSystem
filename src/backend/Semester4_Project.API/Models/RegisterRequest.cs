// The data the frontend sends to create an account.
// The [Required], [EmailAddress] and [MinLength] rules are checked automatically.

using System.ComponentModel.DataAnnotations;

namespace Semester4_Project.API.Models;

public class RegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string Password { get; set; } = "";

    [Required]
    public string Role { get; set; } = "";
}
