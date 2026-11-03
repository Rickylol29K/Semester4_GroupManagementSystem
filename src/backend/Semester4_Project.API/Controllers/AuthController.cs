//register, login, logout and who is logged in

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semester4_Project.API.Models;
using Semester4_Project.Business.Models;
using Semester4_Project.Business.Services;

namespace Semester4_Project.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AccountService _accounts;

    public AuthController(AccountService accounts)
    {
        _accounts = accounts;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _accounts.RegisterAsync(request.Email, request.Password, request.Role);
        if (!result.Success)
        {
            return BadRequest(new { error = result.Error });
        }

        await SignInAsync(result.User!, false);
        return Ok(result.User);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _accounts.LoginAsync(request.Email, request.Password);
        if (!result.Success)
        {
            return Unauthorized(new { error = result.Error });
        }

        await SignInAsync(result.User!, request.StaySignedIn);
        return Ok(result.User);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    //returns who is logged in, 401 if nobody
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new UserInfo
        {
            Id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
            Email = User.FindFirstValue(ClaimTypes.Email)!,
            Role = User.FindFirstValue(ClaimTypes.Role)!,
        });
    }

    //puts id, email and role in the login cookie
    private async Task SignInAsync(UserInfo user, bool staySignedIn)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = staySignedIn });
    }
}
