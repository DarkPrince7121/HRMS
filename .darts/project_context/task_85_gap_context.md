# Gap Context — Implement an automatic database initialization process that executes during application startup. The application must read the connection string from `appsettings.json`, create the `DbContext` using Dependency Injection, verify the SQL Server connection, create the database if it does not exist, automatically apply all pending Entity Framework Core migrations, validate the schema, and execute all required seed data (roles, default admin user, lookup tables, application settings, and other mandatory reference data) in an idempotent manner without creating duplicates. Keep all initialization, migration, and seed logic within the **HRMS.Data** project using dedicated initializer and seed classes, and invoke only a single initialization service from `Program.cs` in **HRMS.UI**. The application should be fully self-initializing for development, allowing any developer to clone the repository, configure `appsettings.json`, run the application, and have the database, schema, and initial data prepared automatically without manually creating the database or executing Entity Framework migration commands.

**Date:** 2025-02-11  |  **Task ID:** 85  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- .NET 6.0 (ASP.NET Core MVC)
- Entity Framework Core 6.0.0
- SQL Server (referenced in code as `UseSqlServer`, although requirement mentions PostgreSQL, the current implementation uses SQL Server)
- KnockoutJS (Frontend)

### Existing Modules & Features
- **HRMS.Data** (`HRMS.Data/`): Contains `ApplicationDbContext`, EF Core Migrations, and Entity Configurations (e.g., `RoleConfiguration.cs`).
- **HRMS.Models** (`HRMS.Models/`): Contains domain entities (`User`, `Role`, `Employee`, etc.) and DTOs.
- **HRMS.Services** (`HRMS.Services/`): Business logic services for Attendance, Payroll, Leave management, etc.
- **HRMS.UI** (`HRMS.UI/`): Web application entry point with MVC Controllers and KnockoutJS view models.
- **HRMS.Web** (`HRMS.Web/`): Secondary web/worker project containing background jobs.

### Prior Context
No prior gap analysis found for Task 85 specifically, but the project has an established structure with Entity Framework Core migrations and basic data configurations (seed data for roles in `RoleConfiguration.cs`).

## Requirements Analysis

### Extracted Requirements
1. **Automatic Initialization on Startup:** The application must trigger DB setup automatically when the web application starts.
2. **Configuration driven:** Connection string must be retrieved from `appsettings.json`.
3. **DI-based DbContext:** Initialization must use `DbContext` resolved via Dependency Injection.
4. **Database Readiness:** Verify connectivity, create the database if it doesn't exist, and apply all pending EF Core migrations.
5. **Schema Validation:** Ensure the database schema matches the model.
6. **Idempotent Data Seeding:** Seed roles, admin user, lookup tables, and settings without creating duplicates.
7. **Encapsulation in HRMS.Data:** All logic (initializer, seeders) must reside in the `HRMS.Data` project.
8. **Minimal UI Coupling:** `Program.cs` in `HRMS.UI` should only call a single initialization service.
9. **Self-initializing:** Developers should only need to configure the connection string to have a working DB.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Read connection string from `appsettings.json` | Already Exists | `HRMS.UI/Program.cs` | Already implemented using `builder.Configuration.GetConnectionString`. |
| Create `DbContext` using DI | Already Exists | `HRMS.UI/Program.cs` | `AddDbContext` is already configured. |
| Automatic migration on startup | Needs Modification | `HRMS.UI/Program.cs` | A basic `context.Database.Migrate()` call exists in `Program.cs`, but lacks comprehensive seed/validation logic and isn't encapsulated in a service. |
| Verify connection and create DB | Needs Modification | `HRMS.UI/Program.cs` | `Migrate()` handles creation, but explicit verification and schema validation are missing. |
| Idempotent Seed Data (Roles, Admin, etc.) | Needs Modification | `HRMS.Data/Configurations/` | Some seeding exists in `RoleConfiguration.cs` via `HasData`, but dynamic/programmatic seeding (Admin user, settings) is missing. |
| Logic encapsulated in `HRMS.Data` | New Development | — | Need a dedicated service/initializer in `HRMS.Data` project. |
| Invoke single service from `Program.cs` | Needs Modification | `HRMS.UI/Program.cs` | Current inline logic needs to be replaced with a single service call. |

## Tech Stack & Implementation

### Database Initializer Service — New Development
- **Approach:** Create a service interface and implementation within `HRMS.Data`. This service will encapsulate the logic to ensure the database is created, migrate it to the latest version, and call separate seeder classes. It will use `IServiceProvider` or specific services to perform checks.
- **Existing files to modify:** None (New classes in `HRMS.Data`)
- **New dependencies:** None

### Data Seeding Logic — Needs Modification
- **Approach:** Implement dedicated seed classes in `HRMS.Data` that use the `DbContext` to check for existence before inserting (idempotency). This should handle more complex data than `HasData` in configurations (like Default Admin with hashed passwords).
- **Existing files to modify:** `HRMS.Data/ApplicationDbContext.cs` (to potentially call seeders or expose initialization entry points).
- **New dependencies:** None

### Startup Integration — Needs Modification
- **Approach:** Modify `Program.cs` to resolve the new initialization service from `HRMS.Data` and invoke its initialization method during the host startup phase.
- **Existing files to modify:** `HRMS.UI/Program.cs`
- **New dependencies:** None

## Summary
The project is a Brownfield ASP.NET Core application using Entity Framework Core. While it already contains a basic migration call in `Program.cs` and some static data seeding in configurations, it lacks a robust, encapsulated, and idempotent initialization process. 

This task requires refactoring the startup logic to move database readiness checks and seeding into the `HRMS.Data` project. The implementation will focus on creating a service that can be easily invoked from the UI layer, ensuring that any developer can get the environment running immediately after providing a connection string. The seeding process will be upgraded from static EF configurations to programmatic logic to support idempotent creation of the default admin user and mandatory lookup data.
