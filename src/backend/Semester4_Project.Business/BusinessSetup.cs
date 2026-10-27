using Microsoft.Extensions.DependencyInjection;
using Semester4_Project.DataAccess;

namespace Semester4_Project.Business;

// Lets the API set up the business and data layers without knowing about the database code.
public static class BusinessSetup
{
    public static void AddBusiness(IServiceCollection services, string? connectionString)
    {
        DataAccessSetup.AddDataAccess(services, connectionString);
        services.AddScoped<AccountService>();
    }

    public static void UpdateDatabase(IServiceProvider services)
    {
        DataAccessSetup.UpdateDatabase(services);
    }
}
