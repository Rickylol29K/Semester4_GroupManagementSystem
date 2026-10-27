using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Semester4_Project.IntegrationTests;

// Tests for user story 1: account registration and login.
public class AuthTests : IClassFixture<TestAppFactory>
{
    private readonly TestAppFactory _factory;

    public AuthTests(TestAppFactory factory)
    {
        _factory = factory;
    }

    private static Task<HttpResponseMessage> RegisterAsync(HttpClient client, string email, string password, string role)
    {
        return client.PostAsJsonAsync("/api/auth/register", new { email, password, role });
    }

    private static Task<HttpResponseMessage> LoginAsync(HttpClient client, string email, string password)
    {
        return client.PostAsJsonAsync("/api/auth/login", new { email, password });
    }

    [Fact]
    public async Task Register_WithEmailAndPassword_CreatesAccountAndLogsIn()
    {
        var client = _factory.CreateClient();

        var response = await RegisterAsync(client, "register@test.nl", "password123", "Student");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var me = await client.GetFromJsonAsync<UserResponse>("/api/auth/me");
        Assert.Equal("register@test.nl", me!.Email);
        Assert.Equal("Student", me.Role);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ReturnsBadRequest()
    {
        var client = _factory.CreateClient();
        await RegisterAsync(client, "duplicate@test.nl", "password123", "Student");

        var response = await RegisterAsync(client, "duplicate@test.nl", "password123", "Student");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithShortPassword_ReturnsBadRequest()
    {
        var client = _factory.CreateClient();

        var response = await RegisterAsync(client, "short@test.nl", "short", "Student");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithUnknownRole_ReturnsBadRequest()
    {
        var client = _factory.CreateClient();

        var response = await RegisterAsync(client, "admin@test.nl", "password123", "Admin");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithCorrectPassword_ReturnsUser()
    {
        await RegisterAsync(_factory.CreateClient(), "login@test.nl", "password123", "Teacher");
        var client = _factory.CreateClient();

        var response = await LoginAsync(client, "login@test.nl", "password123");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var me = await client.GetFromJsonAsync<UserResponse>("/api/auth/me");
        Assert.Equal("Teacher", me!.Role);
    }

    [Fact]
    public async Task Login_WithWrongPassword_ReturnsUnauthorized()
    {
        await RegisterAsync(_factory.CreateClient(), "wrong@test.nl", "password123", "Student");
        var client = _factory.CreateClient();

        var response = await LoginAsync(client, "wrong@test.nl", "not-the-password");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_MeansUserIsNoLongerLoggedIn()
    {
        var client = _factory.CreateClient();
        await RegisterAsync(client, "logout@test.nl", "password123", "Student");

        await client.PostAsync("/api/auth/logout", null);

        var response = await client.GetAsync("/api/auth/me");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Student_CannotUseTeacherEndpoints()
    {
        var client = _factory.CreateClient();
        await RegisterAsync(client, "student-perm@test.nl", "password123", "Student");

        var response = await client.GetAsync("/api/teacher/students");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Teacher_CanUseTeacherEndpoints()
    {
        var client = _factory.CreateClient();
        await RegisterAsync(client, "teacher-perm@test.nl", "password123", "Teacher");

        var response = await client.GetAsync("/api/teacher/students");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task NotLoggedIn_CannotUseTeacherEndpoints()
    {
        var response = await _factory.CreateClient().GetAsync("/api/teacher/students");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private class UserResponse
    {
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
