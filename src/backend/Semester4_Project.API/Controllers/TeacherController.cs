using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semester4_Project.Business;

namespace Semester4_Project.API.Controllers;

// Only teachers can use these endpoints. Students get 403 Forbidden.
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
