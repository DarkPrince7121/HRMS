using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Data.Interfaces;
using HRMS.Data.Implementations;
using HRMS.Services;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

// Enable legacy timestamp behavior for Npgsql to handle Kind=Unspecified as UTC
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Added support for API and MVC controllers

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataFactory, DataFactoryServiceProvider>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();
builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISettingService, SettingService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Apply migrations and initialize database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        
        var initializer = services.GetRequiredService<IDatabaseInitializer>();
        await initializer.InitializeAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();