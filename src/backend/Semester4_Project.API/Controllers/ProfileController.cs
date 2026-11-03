//student can see and change their own profile

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semester4_Project.Business.Models;
using Semester4_Project.Business.Services;

namespace Semester4_Project.API.Controllers;

[ApiController]
[Route("api/student/profile")]
[Authorize(Roles = Roles.Student)]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profiles;

    public ProfileController(ProfileService profiles)
    {
        _profiles = profiles;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _profiles.GetAsync(CurrentUserId()));
    }

    [HttpPut]
    public async Task<IActionResult> Save(ProfileInfo profile)
    {
        return Ok(await _profiles.SaveAsync(CurrentUserId(), profile));
    }

    //id of the logged in user from the cookie
    private int CurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
