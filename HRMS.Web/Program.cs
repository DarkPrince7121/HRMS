using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using Quartz;
using HRMS.Web.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataFactory, DataFactoryServiceProvider>();

// Quartz.NET configuration
builder.Services.AddQuartz(q =>
{
    // q.UseMicrosoftDependencyInjectionJobFactory();

    // Create a "key" for the job
    var jobKey = new JobKey("AttendanceProcessingJob");

    // Register the job with the DI container
    q.AddJob<AttendanceProcessingJob>(opts => opts.WithIdentity(jobKey));

    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AttendanceProcessingJob-trigger")
        // This schedule runs every minute for demonstration purposes
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever()));
});

// Quartz.NET hosted service
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Apply migrations and create database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or creating the database.");
    }
}

app.MapGet("/", () => "HRMS Background Jobs Service Running with Quartz.NET");

app.Run();