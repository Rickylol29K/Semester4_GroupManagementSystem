// Connects the app to SQL Server and creates or updates the tables when the app starts.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Semester4_Project.DataAccess.Repositories;

namespace Semester4_Project.DataAccess;

public static class DataAccessSetup
{
    public static void AddDataAccess(IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<UserRepository>();
        services.AddScoped<ProfileRepository>();
    }

    // Creates the database and tables if they don't exist yet.
    public static void UpdateDatabase(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // The tests use an in-memory database, which has no migrations.
        if (db.Database.IsRelational())
        {
            db.Database.Migrate();
        }
    }
}
