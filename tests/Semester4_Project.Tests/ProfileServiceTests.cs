//unit tests for ProfileService

using Semester4_Project.Business.Models;
using Semester4_Project.Business.Services;
using Semester4_Project.DataAccess;
using Semester4_Project.DataAccess.Repositories;
using Xunit;

namespace Semester4_Project.Tests;

public class ProfileServiceTests
{
    private readonly AppDbContext _db;
    private readonly ProfileService _service;

    public ProfileServiceTests()
    {
        _db = TestDatabase.Create();
        _service = new ProfileService(new ProfileRepository(_db));
    }

    [Fact]
    public async Task Get_WithoutProfile_ReturnsEmptyProfile()
    {
        var profile = await _service.GetAsync(1);

        Assert.Equal("", profile.FullName);
        Assert.Empty(profile.Skills);
        Assert.Empty(profile.Availability);
    }

    [Fact]
    public async Task Save_ThenGet_ReturnsSameData()
    {
        var info = new ProfileInfo
        {
            FullName = "Sam Student",
            Skills = new List<string> { "Back end", "Testing" },
            LearningGoals = "Learn React",
            Availability = new List<string> { "Monday", "Friday" },
        };

        await _service.SaveAsync(1, info);
        var profile = await _service.GetAsync(1);

        Assert.Equal("Sam Student", profile.FullName);
        Assert.Equal(new List<string> { "Back end", "Testing" }, profile.Skills);
        Assert.Equal("Learn React", profile.LearningGoals);
        Assert.Equal(new List<string> { "Monday", "Friday" }, profile.Availability);
    }

    [Fact]
    public async Task Save_RemovesSpacesAroundText()
    {
        var info = new ProfileInfo { FullName = "   Sam Student  " };

        var profile = await _service.SaveAsync(1, info);

        Assert.Equal("Sam Student", profile.FullName);
    }

    [Fact]
    public async Task Save_Twice_UpdatesTheSameProfile()
    {
        await _service.SaveAsync(1, new ProfileInfo { FullName = "Old Name" });
        await _service.SaveAsync(1, new ProfileInfo { FullName = "New Name" });

        var profile = await _service.GetAsync(1);

        Assert.Equal("New Name", profile.FullName);
        Assert.Equal(1, _db.StudentProfiles.Count());
    }

    [Fact]
    public async Task Profiles_OfDifferentStudents_AreSeparate()
    {
        await _service.SaveAsync(1, new ProfileInfo { FullName = "First Student" });
        await _service.SaveAsync(2, new ProfileInfo { FullName = "Second Student" });

        Assert.Equal("First Student", (await _service.GetAsync(1)).FullName);
        Assert.Equal("Second Student", (await _service.GetAsync(2)).FullName);
    }
}
