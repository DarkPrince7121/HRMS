using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HRMS.Data.Interfaces;
using HRMS.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HRMS.Data.Implementations
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(ApplicationDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedAdminUserAsync();
            await SeedSystemSettingsAsync();
            await SeedLookupTablesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var roles = new List<string> { "Admin", "Manager", "Employee" };
            foreach (var roleName in roles)
            {
                if (!await _context.Roles.AnyAsync(r => r.Name == roleName))
                {
                    _context.Roles.Add(new Role { Name = roleName });
                    _logger.LogInformation("Seeding Role: {RoleName}", roleName);
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedAdminUserAsync()
        {
            if (!await _context.Users.AnyAsync(u => u.Username == "admin"))
            {
                var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                if (adminRole != null)
                {
                    var adminUser = new User
                    {
                        Username = "admin",
                        Email = "admin@hrms.com",
                        PasswordHash = "AQAAAAEAACcQAAAAEGATozi5KgzOKV9UMvs2Y6n79w8TfCJt/Q+BdA5pjq52YrGoPEkTNHqMC5yFraq7RQ==", // Hashed version of 'manoj'
                        RoleId = adminRole.Id
                    };
                    _context.Users.Add(adminUser);
                    _logger.LogInformation("Seeding Default Admin User");
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedSystemSettingsAsync()
        {
            var defaultSettings = new List<SystemSetting>
            {
                new SystemSetting { Key = "CompanyName", Value = "HRMS Corp", Description = "The name of the company." },
                new SystemSetting { Key = "Currency", Value = "USD", Description = "Base currency for payroll." },
                new SystemSetting { Key = "WorkingHoursPerDay", Value = "8", Description = "Standard working hours per day." }
            };

            foreach (var setting in defaultSettings)
            {
                if (!await _context.SystemSettings.AnyAsync(s => s.Key == setting.Key))
                {
                    _context.SystemSettings.Add(setting);
                    _logger.LogInformation("Seeding System Setting: {Key}", setting.Key);
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedLookupTablesAsync()
        {
            // Seed Departments if empty
            if (!await _context.Departments.AnyAsync())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "Human Resources", Code = "HR" },
                    new Department { Name = "Information Technology", Code = "IT" },
                    new Department { Name = "Finance", Code = "FIN" }
                };
                _context.Departments.AddRange(departments);
                _logger.LogInformation("Seeding default Departments");
            }

            // Seed Designations if empty
            if (!await _context.Designations.AnyAsync())
            {
                var designations = new List<Designation>
                {
                    new Designation { Name = "Software Engineer" },
                    new Designation { Name = "HR Manager" },
                    new Designation { Name = "Accountant" }
                };
                _context.Designations.AddRange(designations);
                _logger.LogInformation("Seeding default Designations");
            }

            await _context.SaveChangesAsync();
        }
    }
}