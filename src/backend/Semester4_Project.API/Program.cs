//start of the backend, sets everything up and runs the server

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Semester4_Project.Business.Services;
using Semester4_Project.DataAccess;
using Semester4_Project.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//database, repositories and services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProfileRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ProfileService>();

//login cookie, give 401/403 instead of redirecting to a login page
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

//create or update the tables
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.IsRelational())
    {
        db.Database.Migrate();
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

//so the tests can start the app
public partial class Program { }
