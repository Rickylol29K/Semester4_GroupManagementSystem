//unit tests for AccountService

using Semester4_Project.Business.Services;
using Semester4_Project.DataAccess;
using Semester4_Project.DataAccess.Repositories;
using Xunit;

namespace Semester4_Project.Tests;

public class AccountServiceTests
{
    private readonly AppDbContext _db;
    private readonly AccountService _service;

    //xunit runs this before every test, so every test gets a new database
    public AccountServiceTests()
    {
        _db = TestDatabase.Create();
        _service = new AccountService(new UserRepository(_db));
    }

    [Fact]
    public async Task Register_WithValidData_Succeeds()
    {
        var result = await _service.RegisterAsync("sam@test.nl", "password123", "Student");

        Assert.True(result.Success);
        Assert.Equal("sam@test.nl", result.User!.Email);
        Assert.Equal("Student", result.User.Role);
    }

    [Fact]
    public async Task Register_MakesEmailLowercaseAndTrimmed()
    {
        var result = await _service.RegisterAsync("  Sam@Test.NL ", "password123", "Student");

        Assert.Equal("sam@test.nl", result.User!.Email);
    }

    [Fact]
    public async Task Register_DoesNotStoreThePlainPassword()
    {
        await _service.RegisterAsync("hash@test.nl", "password123", "Student");

        var user = _db.Users.Single();
        Assert.NotEqual("password123", user.PasswordHash);
    }

    [Fact]
    public async Task Register_WithUnknownRole_GivesError()
    {
        var result = await _service.RegisterAsync("admin@test.nl", "password123", "Admin");

        Assert.False(result.Success);
        Assert.Null(result.User);
    }

    [Fact]
    public async Task Register_WithExistingEmail_GivesError()
    {
        await _service.RegisterAsync("double@test.nl", "password123", "Student");

        var result = await _service.RegisterAsync("double@test.nl", "password123", "Student");

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_WithCorrectPassword_Succeeds()
    {
        await _service.RegisterAsync("login@test.nl", "password123", "Teacher");

        var result = await _service.LoginAsync("login@test.nl", "password123");

        Assert.True(result.Success);
        Assert.Equal("Teacher", result.User!.Role);
    }

    [Fact]
    public async Task Login_WithWrongPassword_GivesError()
    {
        await _service.RegisterAsync("wrong@test.nl", "password123", "Student");

        var result = await _service.LoginAsync("wrong@test.nl", "wrongpassword");

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_WithUnknownEmail_GivesError()
    {
        var result = await _service.LoginAsync("nobody@test.nl", "password123");

        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetStudents_OnlyReturnsStudents()
    {
        await _service.RegisterAsync("student1@test.nl", "password123", "Student");
        await _service.RegisterAsync("student2@test.nl", "password123", "Student");
        await _service.RegisterAsync("teacher@test.nl", "password123", "Teacher");

        var students = await _service.GetStudentsAsync();

        Assert.Equal(2, students.Count);
        Assert.All(students, s => Assert.Equal("Student", s.Role));
    }
}
