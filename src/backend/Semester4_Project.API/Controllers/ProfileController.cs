// Lets a logged in student see and change their own profile.
// Teachers get 403 Forbidden, logged out users get 401.

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semester4_Project.API.Models;
using Semester4_Project.Business;

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
    public async Task<IActionResult> Save(ProfileRequest request)
    {
        var info = new ProfileInfo
        {
            FullName = request.FullName,
            Skills = request.Skills,
            Interests = request.Interests,
            Strengths = request.Strengths,
            Weaknesses = request.Weaknesses,
            LearningGoals = request.LearningGoals,
            WorkingPreferences = request.WorkingPreferences,
            Availability = request.Availability,
        };

        return Ok(await _profiles.SaveAsync(CurrentUserId(), info));
    }

    // The id of the logged in user, taken from the login cookie.
    private int CurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
