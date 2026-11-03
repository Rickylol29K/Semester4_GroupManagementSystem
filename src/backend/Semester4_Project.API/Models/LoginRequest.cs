//what the frontend sends to log in

using System.ComponentModel.DataAnnotations;

namespace Semester4_Project.API.Models;

public class LoginRequest
{
    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    public bool StaySignedIn { get; set; }
}
