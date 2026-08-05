# Gap Context — As mentioned in the previous task, I'm still facing the same issue that, no tables have been created. and getting the below error.
fail: Microsoft.EntityFrameworkCore.Database.Command[20102]
      Failed executing DbCommand (24ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Roles] AS [r]) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
fail: Microsoft.EntityFrameworkCore.Query[10100]
      An exception occurred while iterating over the results of a query for context type 'HRMS.Data.ApplicationDbContext'.
      Microsoft.Data.SqlClient.SqlException (0x80131904): Invalid e 'Roles'.

Please do the necessary changes to create the required tables and data within it as well.
**Date:** 2025-01-24  |  **Task ID:** 89  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 6.0 (Web API / MVC)
- **Database:** PostgreSQL (intended) / SQL Server (currently configured)
- **ORM:** Entity Framework Core 6.0
- **Frontend:** KnockoutJS

### Existing Modules & Features
- **HRMS.Data** (`HRMS.Data/`): Contains Entity Framework Core context, migrations, and database initialization logic.
- **HRMS.Models** (`HRMS.Models/`): Contains domain entities and DTOs.
- **HRMS.Services** (`HRMS.Services/`): Contains business logic services.
- **HRMS.UI** (`HRMS.UI/`): ASP.NET Core MVC application with KnockoutJS frontend.
- **HRMS.Web** (`HRMS.Web/`): Likely a background processing or worker service.

### Prior Context
The project is in a state where the database schema is not being correctly applied despite having migrations and an initializer. Previous tasks (75, 85) results are present in the root, indicating ongoing efforts to fix database connectivity and initialization.

## Requirements Analysis

### Extracted Requirements
1. **Fix Table Creation:** Resolve the `SqlException: Invalid object name 'Roles'` by ensuring the database schema is correctly created in the target database.
2. **Switch to PostgreSQL:** Align the implementation with the target framework context (PostgreSQL) as the current configuration uses SQL Server (localdb).
3. **Data Seeding:** Ensure that required initial data (Roles, Admin User, Settings, etc.) is populated after table creation.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Create required tables | Needs Modification | `HRMS.UI/Program.cs`, `HRMS.Data/Implementations/DatabaseInitializer.cs` | The current initialization logic fails to create the actual schema or is targeting the wrong DB. |
| Seed required data | Already Exists | `HRMS.Data/Implementations/DataSeeder.cs` | Logic exists but cannot run because tables are missing. |
| Use PostgreSQL | Needs Modification | `HRMS.UI/Program.cs`, `HRMS.UI/appsettings.json` | Currently configured for SQL Server/LocalDB. |

## Tech Stack & Implementation

### Fix Table Creation & Database Migration — Needs Modification
- **Approach:** Switch the application to use Npgsql (PostgreSQL provider) instead of SQL Server. Update the connection string to point to a PostgreSQL instance. Modify the `DatabaseInitializer` to ensure a clean migration state or force schema creation. The error "Invalid object name 'Roles'" specifically indicates that the code is querying a table that does not exist in the database it is connected to.
- **Existing files to modify:** `HRMS.UI/Program.cs`, `HRMS.UI/appsettings.json`, `HRMS.Data/Implementations/DatabaseInitializer.cs`
- **New dependencies:** None (Npgsql is already in `HRMS.Data.csproj`)

### Data Seeding — Already Exists
- **Approach:** The seeding logic is already implemented in `DataSeeder.cs`. It will be triggered by `DatabaseInitializer.cs` once the schema creation issue is resolved.
- **Existing files to modify:** None
- **New dependencies:** None

## Summary
The project is a Brownfield ASP.NET Core application using Entity Framework Core. The primary issue is a mismatch between the expected database schema and the actual state of the database, resulting in "Invalid object name" errors when the application tries to check for existing data (e.g., in the `Roles` table) during startup.

The task requires correcting the database provider to PostgreSQL as per the requirements, updating the connection string, and ensuring that the EF Core `MigrateAsync` or `EnsureCreatedAsync` calls in the `DatabaseInitializer` successfully create the tables. Once the schema is applied, the existing `DataSeeder` will handle the population of initial data. The implementation is primarily a configuration and infrastructure fix within the `HRMS.UI` and `HRMS.Data` projects.