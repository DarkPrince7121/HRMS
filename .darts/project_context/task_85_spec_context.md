# Spec Context — Task 85
**Generated:** 2025-05-15  |  **Framework:** .NET 6.0 (ASP.NET Core MVC), EF Core, SQL Server  |  **Tasks:** 3

## Gap Analysis Summary
The current application (Brownfield) has basic database migration logic directly in `Program.cs` and some static data seeding via EF Core configuration files (e.g., `RoleConfiguration.cs`). The gap analysis identified the need for a robust, encapsulated, and idempotent database initialization process. This requires moving initialization logic out of the UI layer and into the `HRMS.Data` project, implementing an initialization service that handles connection verification, database creation, migration application, and programmatic seeding (e.g., default Admin user, system settings). The UI project should only invoke a single entry point for this initialization.

## Task Plan

### Module: Core Infrastructure

#### Feature: Database Self-Initialization

**T-001: Implement Database Initializer Service in HRMS.Data**
- **Description:** Create the `IDatabaseInitializer` interface and its implementation `DatabaseInitializer` within the `HRMS.Data` project. This service will verify the SQL Server connection, ensure the database is created, apply all pending Entity Framework Core migrations, and perform schema validation.
- **Files to create:** `HRMS.Data/Interfaces/IDatabaseInitializer.cs`, `HRMS.Data/Implementations/DatabaseInitializer.cs`
- **Files to modify:** `HRMS.Data/HRMS.Data.csproj`
- **Depends on:** None
- **Acceptance criteria:**
  - `DatabaseInitializer` can be instantiated via DI with `ApplicationDbContext`.
  - `InitializeAsync()` method successfully verifies connectivity to SQL Server.
  - `InitializeAsync()` applies pending migrations to the database.
- **Wiring:**
  - Imports from: `HRMS.Data/ApplicationDbContext.cs`
  - Imported by: `HRMS.UI/Program.cs`
  - API routes: None
  - DB tables: `__EFMigrationsHistory`
  - Env vars: Connection string from `appsettings.json`

**T-002: Implement Idempotent Data Seeding Logic**
- **Description:** Create a programmatic seeding mechanism within `HRMS.Data`. This will include a `DataSeeder` class that checks for the existence of records before insertion to ensure idempotency. It will seed mandatory reference data: Default Admin User (with hashed password), Roles (if not already handled by EF configuration), default System Settings, and Lookup Tables.
- **Files to create:** `HRMS.Data/Implementations/DataSeeder.cs`, `HRMS.Data/Interfaces/IDataSeeder.cs`
- **Files to modify:** `HRMS.Data/HRMS.Data.csproj`
- **Depends on:** T-001
- **Acceptance criteria:**
  - Default Admin user is created if no admin exists.
  - Default roles are present in the database.
  - System settings table is populated with default entries if empty.
  - Running the seeder multiple times does not result in duplicate records.
- **Wiring:**
  - Imports from: `HRMS.Models/Entities/*.cs`, `HRMS.Data/ApplicationDbContext.cs`
  - Imported by: `HRMS.Data/Implementations/DatabaseInitializer.cs`
  - API routes: None
  - DB tables: `Users`, `Roles`, `SystemSettings`, `Departments`, `Designations`
  - Env vars: None

**T-003: Refactor Startup to Invoke Initialization Service**
- **Description:** Register the initialization services in the DI container and modify `HRMS.UI/Program.cs` to resolve `IDatabaseInitializer` and call its initialization method. Remove the existing inline `context.Database.Migrate()` call from `Program.cs`.
- **Files to create:** None
- **Files to modify:** `HRMS.UI/Program.cs`
- **Depends on:** T-001, T-002
- **Acceptance criteria:**
  - Application starts successfully and initializes the database automatically.
  - Inline migration logic is removed from `Program.cs`.
  - Initialization services are registered with Scoped/Transient lifetime as appropriate.
- **Wiring:**
  - Imports from: `HRMS.Data/Interfaces/IDatabaseInitializer.cs`, `HRMS.Data/Implementations/DatabaseInitializer.cs`
  - Imported by: None
  - API routes: None
  - DB tables: None
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Core Infrastructure",
      "features": [
        {
          "feature": "Database Self-Initialization",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Database Initializer Service in HRMS.Data",
              "description": "Create the IDatabaseInitializer interface and its implementation DatabaseInitializer within the HRMS.Data project. This service will verify the SQL Server connection, ensure the database is created, apply all pending Entity Framework Core migrations, and perform schema validation.",
              "files_to_create": [
                "HRMS.Data/Interfaces/IDatabaseInitializer.cs",
                "HRMS.Data/Implementations/DatabaseInitializer.cs"
              ],
              "files_to_modify": [
                "HRMS.Data/HRMS.Data.csproj"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "DatabaseInitializer can be instantiated via DI with ApplicationDbContext.",
                "InitializeAsync() method successfully verifies connectivity to SQL Server.",
                "InitializeAsync() applies pending migrations to the database."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data/ApplicationDbContext.cs"
                ],
                "imported_by": [
                  "HRMS.UI/Program.cs"
                ],
                "api_routes": [],
                "db_tables": [
                  "__EFMigrationsHistory"
                ],
                "env_vars": [
                  "DefaultConnection"
                ]
              }
            },
            {
              "id": "T-002",
              "name": "Implement Idempotent Data Seeding Logic",
              "description": "Create a programmatic seeding mechanism within HRMS.Data. This will include a DataSeeder class that checks for the existence of records before insertion to ensure idempotency. It will seed mandatory reference data: Default Admin User (with hashed password), Roles, default System Settings, and Lookup Tables.",
              "files_to_create": [
                "HRMS.Data/Implementations/DataSeeder.cs",
                "HRMS.Data/Interfaces/IDataSeeder.cs"
              ],
              "files_to_modify": [
                "HRMS.Data/HRMS.Data.csproj"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "Default Admin user is created if no admin exists.",
                "Default roles are present in the database.",
                "System settings table is populated with default entries if empty.",
                "Running the seeder multiple times does not result in duplicate records."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models/Entities/User.cs",
                  "HRMS.Models/Entities/Role.cs",
                  "HRMS.Models/Entities/SystemSetting.cs",
                  "HRMS.Data/ApplicationDbContext.cs"
                ],
                "imported_by": [
                  "HRMS.Data/Implementations/DatabaseInitializer.cs"
                ],
                "api_routes": [],
                "db_tables": [
                  "Users",
                  "Roles",
                  "SystemSettings"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-003",
              "name": "Refactor Startup to Invoke Initialization Service",
              "description": "Register the initialization services in the DI container and modify HRMS.UI/Program.cs to resolve IDatabaseInitializer and call its initialization method. Remove the existing inline context.Database.Migrate() call from Program.cs.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [
                "T-001",
                "T-002"
              ],
              "acceptance_criteria": [
                "Application starts successfully and initializes the database automatically.",
                "Inline migration logic is removed from Program.cs.",
                "Initialization services are registered with Scoped/Transient lifetime as appropriate."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data/Interfaces/IDatabaseInitializer.cs",
                  "HRMS.Data/Implementations/DatabaseInitializer.cs"
                ],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [],
                "env_vars": []
              }
            }
          ]
        }
      ]
    }
  ]
}
```
