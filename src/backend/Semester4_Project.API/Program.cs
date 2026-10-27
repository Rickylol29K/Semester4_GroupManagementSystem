// Start point of the backend. Sets up everything the app needs (database, login cookies,
// controllers) and then starts the web server.

using Microsoft.AspNetCore.Authentication.Cookies;
using Semester4_Project.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

BusinessSetup.AddBusiness(builder.Services, builder.Configuration.GetConnectionString("Default"));

// Logged in users get a cookie. For an API we answer 401/403 instead of redirecting to a login page.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "groupapp.auth";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

BusinessSetup.UpdateDatabase(app.Services);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }
