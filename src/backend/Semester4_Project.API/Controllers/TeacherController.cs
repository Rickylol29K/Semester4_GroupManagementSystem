//only teachers can use this

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semester4_Project.Business.Models;
using Semester4_Project.Business.Services;

namespace Semester4_Project.API.Controllers;

[ApiController]
[Route("api/teacher")]
[Authorize(Roles = Roles.Teacher)]
public class TeacherController : ControllerBase
{
    private readonly AccountService _accounts;

    public TeacherController(AccountService accounts)
    {
        _accounts = accounts;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents()
    {
        return Ok(await _accounts.GetStudentsAsync());
    }
}
