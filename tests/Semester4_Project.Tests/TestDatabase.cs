//makes a new empty in-memory database for every unit test

using Microsoft.EntityFrameworkCore;
using Semester4_Project.DataAccess;

namespace Semester4_Project.Tests;

public static class TestDatabase
{
    public static AppDbContext Create()
    {
        //a random name so tests never share data
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
