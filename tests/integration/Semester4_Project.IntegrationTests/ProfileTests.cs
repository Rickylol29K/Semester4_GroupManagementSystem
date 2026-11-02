// Tests for user story 2 (student profile).
// Each test checks one acceptance criterion by calling the real API.

using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Semester4_Project.IntegrationTests;

public class ProfileTests : IClassFixture<TestAppFactory>
{
    private readonly TestAppFactory _factory;

    public ProfileTests(TestAppFactory factory)
    {
        _factory = factory;
    }

    // Returns a client that is logged in as a new account with the given role.
    private async Task<HttpClient> LoggedInClientAsync(string email, string role)
    {
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/api/auth/register", new { email, password = "password123", role });
        return client;
    }

    private static ProfileData FullProfile()
    {
        return new ProfileData
        {
            FullName = "Sam Student",
            Skills = new List<string> { "Back end", "Testing" },
            Interests = "Games and AI",
            Strengths = "Planning",
            Weaknesses = "Presenting",
            LearningGoals = "Learn React",
            WorkingPreferences = "On campus, in the morning",
            Availability = new List<string> { "Monday", "Thursday" },
        };
    }

    [Fact]
    public async Task NewStudent_GetsEmptyProfile()
    {
        var client = await LoggedInClientAsync("empty-profile@test.nl", "Student");

        var profile = await client.GetFromJsonAsync<ProfileData>("/api/student/profile");

        Assert.Equal("", profile!.FullName);
        Assert.Empty(profile.Skills);
    }

    [Fact]
    public async Task SaveProfile_StoresAllFields()
    {
        var client = await LoggedInClientAsync("save-profile@test.nl", "Student");

        var response = await client.PutAsJsonAsync("/api/student/profile", FullProfile());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var profile = await client.GetFromJsonAsync<ProfileData>("/api/student/profile");
        Assert.Equal("Sam Student", profile!.FullName);
        Assert.Equal(new List<string> { "Back end", "Testing" }, profile.Skills);
        Assert.Equal("Games and AI", profile.Interests);
        Assert.Equal("Planning", profile.Strengths);
        Assert.Equal("Presenting", profile.Weaknesses);
        Assert.Equal("Learn React", profile.LearningGoals);
        Assert.Equal("On campus, in the morning", profile.WorkingPreferences);
        Assert.Equal(new List<string> { "Monday", "Thursday" }, profile.Availability);
    }

    [Fact]
    public async Task SaveProfile_Again_ChangesTheProfile()
    {
        var client = await LoggedInClientAsync("change-profile@test.nl", "Student");
        await client.PutAsJsonAsync("/api/student/profile", FullProfile());

        var changed = FullProfile();
        changed.Skills = new List<string> { "Front end" };
        changed.LearningGoals = "Learn Docker";
        await client.PutAsJsonAsync("/api/student/profile", changed);

        var profile = await client.GetFromJsonAsync<ProfileData>("/api/student/profile");
        Assert.Equal(new List<string> { "Front end" }, profile!.Skills);
        Assert.Equal("Learn Docker", profile.LearningGoals);
    }

    [Fact]
    public async Task Profile_IsStillThereAfterLoggingInAgain()
    {
        var client = await LoggedInClientAsync("relogin-profile@test.nl", "Student");
        await client.PutAsJsonAsync("/api/student/profile", FullProfile());

        var newClient = _factory.CreateClient();
        await newClient.PostAsJsonAsync("/api/auth/login", new { email = "relogin-profile@test.nl", password = "password123" });

        var profile = await newClient.GetFromJsonAsync<ProfileData>("/api/student/profile");
        Assert.Equal("Sam Student", profile!.FullName);
    }

    [Fact]
    public async Task Students_OnlySeeTheirOwnProfile()
    {
        var first = await LoggedInClientAsync("own-profile-1@test.nl", "Student");
        await first.PutAsJsonAsync("/api/student/profile", FullProfile());
        var second = await LoggedInClientAsync("own-profile-2@test.nl", "Student");

        var profile = await second.GetFromJsonAsync<ProfileData>("/api/student/profile");

        Assert.Equal("", profile!.FullName);
    }

    [Fact]
    public async Task SaveProfile_WithTooLongName_ReturnsBadRequest()
    {
        var client = await LoggedInClientAsync("long-name@test.nl", "Student");
        var profile = FullProfile();
        profile.FullName = new string('a', 101);

        var response = await client.PutAsJsonAsync("/api/student/profile", profile);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Teacher_CannotUseProfileEndpoints()
    {
        var client = await LoggedInClientAsync("teacher-profile@test.nl", "Teacher");

        var response = await client.GetAsync("/api/student/profile");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task NotLoggedIn_CannotUseProfileEndpoints()
    {
        var response = await _factory.CreateClient().GetAsync("/api/student/profile");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private class ProfileData
    {
        public string FullName { get; set; } = "";
        public List<string> Skills { get; set; } = new();
        public string Interests { get; set; } = "";
        public string Strengths { get; set; } = "";
        public string Weaknesses { get; set; } = "";
        public string LearningGoals { get; set; } = "";
        public string WorkingPreferences { get; set; } = "";
        public List<string> Availability { get; set; } = new();
    }
}
