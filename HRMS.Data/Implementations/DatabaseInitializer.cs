using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using HRMS.Data.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Hosting;

namespace HRMS.Data.Implementations
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataSeeder _seeder;
        private readonly ILogger<DatabaseInitializer> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public DatabaseInitializer(
            ApplicationDbContext context,
            IDataSeeder seeder,
            ILogger<DatabaseInitializer> logger,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            _context = context;
            _seeder = seeder;
            _logger = logger;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task InitializeAsync()
        {
            try
            {
                _logger.LogInformation("Starting database initialization for PostgreSQL...");

                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    _logger.LogError("Connection string 'DefaultConnection' not found.");
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                }

                if (_environment.IsDevelopment())
                {
                    _logger.LogInformation("Development environment detected. Ensuring fresh PostgreSQL schema.");

                    // Drop and recreate to bypass broken migration history during provider transition
                    //_logger.LogInformation("Deleting existing database if any...");
                    //await _context.Database.EnsureDeletedAsync();

                    _logger.LogInformation("Creating clean PostgreSQL schema from models...");
                    await _context.Database.EnsureCreatedAsync();
                    _logger.LogInformation("Database and schema created successfully.");
                    _logger.LogInformation("Non-development environment. Applying migrations...");
                    await _context.Database.MigrateAsync();
                }
                else
                {
                    _logger.LogInformation("Non-development environment. Applying migrations...");
                    await _context.Database.MigrateAsync();
                }

                _logger.LogInformation("Starting data seeding...");
                await _seeder.SeedAsync();
                _logger.LogInformation("Data seeding completed successfully.");

                _logger.LogInformation("Database initialization completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database initialization.");
                throw;
            }
        }
    }
}