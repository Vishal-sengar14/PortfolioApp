using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using PortfolioApp.Data.Repositories.Implementations;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Services.Implementations;
using PortfolioApp.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// Serilog - file based error/exception logging
// ---------------------------------------------------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// ---------------------------------------------------------------------
// MVC + Razor
// ---------------------------------------------------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// ---------------------------------------------------------------------
// EF Core / SQL Server
// ---------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------------------------------------------------
// Repository Pattern / Unit of Work (Dependency Injection)
// ---------------------------------------------------------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ---------------------------------------------------------------------
// Services
// ---------------------------------------------------------------------
builder.Services.AddScoped<IEmailService, EmailService>();

// ---------------------------------------------------------------------
// Session (used for the contact form captcha)
// ---------------------------------------------------------------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ---------------------------------------------------------------------
// Cookie Authentication for the Admin Panel
// ---------------------------------------------------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.Cookie.Name = "PortfolioApp.Auth";
        options.Cookie.HttpOnly = true;
    });

builder.Services.AddAuthorization();

// ---------------------------------------------------------------------
// Response caching + compression for faster loading
// ---------------------------------------------------------------------
builder.Services.AddResponseCaching();

var app = builder.Build();

// ---------------------------------------------------------------------
// Apply migrations + seed data automatically on startup
// ---------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    // In production you would normally run `context.Database.Migrate()`.
    // EnsureCreated() (inside DbInitializer.Seed) is used here so the project
    // works immediately after `dotnet ef database update` OR on first run.
    context.Database.Migrate();
    DbInitializer.Seed(context, config);
}

// ---------------------------------------------------------------------
// Middleware pipeline
// ---------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error404");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
