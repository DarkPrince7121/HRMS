# Gap Context — Database Created Successfully, but Tables Missing

**Date:** 2025-05-14  |  **Task ID:** 86  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 7.0/8.0 (Entity Framework Core)
- **Database:** PostgreSQL (currently misconfigured in code as SQL Server)
- **Frontend:** KnockoutJS, ASP.NET MVC Views (Razor)
- **Architecture:** Layered (Data, Models, Services, UI/Web)

### Existing Modules & Features
- **Data Layer** (`HRMS.Data`): Contains `ApplicationDbContext`, EF Configurations for all entities, Migrations, and Database Initialization logic.
- **Models Layer** (`HRMS.Models`): Contains Domain Entities (User, Role, Employee, etc.) and DTOs for requests/responses.
- **Service Layer** (`HRMS.Services`): Business logic for Attendance, Leaves, Payroll, Employees, etc.
- **UI Layer** (`HRMS.UI`): MVC Controllers and KnockoutJS scripts for the frontend.
- **Account Module** (`HRMS.UI/Controllers/AccountController.cs`): Logic for Login/Logout.

### Prior Context
No prior analysis found for this project in `.darts/project_context/`.

## Requirements Analysis

### Extracted Requirements
1. **Apply Initial Migrations:** The database exists but only contains the `__EFMigrationsHistory` table. All other tables (User, Role, Employee, etc.) must be created.
2. **Database Configuration:** Use existing configuration files in `HRMS.Data/Configurations` to define table structures.
3. **Data Seeding:** Apply initial migrations for User and Role data (Seed Admin User and Roles).
4. **Login/Register Functionality:** Ensure that authentication (Login) works as expected after the database is populated.
5. **Infrastructure Fix (Implicit):** The current `Program.cs` is calling `UseSqlServer`, but the target environment and `appsettings.json` specify PostgreSQL. This must be aligned for migrations to run against the correct DB engine.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Create all tables | Needs Modification | `HRMS.Data/ApplicationDbContext.cs`, `HRMS.UI/Program.cs` | Tables defined in context but not synced with DB. EF provider mismatch found. |
| Apply Configurations | Already Exists | `HRMS.Data/Configurations/` | Configurations are present and applied in `OnModelCreating`. |
| Seed User/Role data | Already Exists | `HRMS.Data/Implementations/DataSeeder.cs` | Logic to seed "Admin" and roles exists but hasn't been successfully persisted. |
| Login Functionality | Needs Modification | `HRMS.UI/Controllers/AccountController.cs` | Controller exists but expects plain-text comparison while seeder uses hashes. |
| PostgreSQL Integration | Needs Modification | `HRMS.UI/Program.cs` | Needs to switch from `UseSqlServer` to `UseNpgsql`. |

## Tech Stack & Implementation

### Database Schema and Provider — Needs Modification
- **Approach:** Update the DI container to use the Npgsql provider for PostgreSQL. Ensure migrations are generated/applied for the PostgreSQL provider specifically. Since `DatabaseInitializer.InitializeAsync()` is already called at startup, fixing the provider and connection string will allow `_context.Database.MigrateAsync()` to create the tables.
- **Existing files to modify:** `HRMS.UI/Program.cs`, `HRMS.UI/appsettings.json`
- **New dependencies:** `Npgsql.EntityFrameworkCore.PostgreSQL`

### Data Seeding and Authentication — Needs Modification
- **Approach:** Verify `DataSeeder.cs` logic. The `AccountController` currently compares `user.PasswordHash` directly with `request.Password` (plain text), whereas `DataSeeder` stores a hash. The login logic needs to be updated to either use a password hasher or align the comparison method.
- **Existing files to modify:** `HRMS.UI/Controllers/AccountController.cs`
- **New dependencies:** None

## Summary
The project is a Brownfield HRMS application with a complete set of Entity Framework configurations and seeding logic already in place. However, the system is in a "broken" state where the database schema has not been applied beyond the migration history table. This is primarily due to a mismatch between the target database (PostgreSQL) and the configured database provider in the code (SQL Server).

The task requires aligning the infrastructure to use PostgreSQL, which will then allow the existing `DatabaseInitializer` to apply migrations and seed the necessary User and Role data. Additionally, the authentication logic in the `AccountController` requires a small correction to ensure it can successfully validate the seeded Admin user. Once these infrastructure and logic alignment fixes are applied, the login/register functionality will be fully operational.