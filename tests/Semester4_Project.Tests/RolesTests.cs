//unit tests for the Roles class

using Semester4_Project.Business.Models;
using Xunit;

namespace Semester4_Project.Tests;

public class RolesTests
{
    [Fact]
    public void Student_IsValid()
    {
        Assert.True(Roles.IsValid("Student"));
    }

    [Fact]
    public void Teacher_IsValid()
    {
        Assert.True(Roles.IsValid("Teacher"));
    }

    [Fact]
    public void UnknownRole_IsNotValid()
    {
        Assert.False(Roles.IsValid("Admin"));
    }

    [Fact]
    public void Role_IsCaseSensitive()
    {
        Assert.False(Roles.IsValid("student"));
    }
}
