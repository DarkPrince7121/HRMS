# Spec Context — Task 89
**Generated:** 2025-01-24  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 3

## Gap Analysis Summary
The project is currently configured to use SQL Server (LocalDB) but the target environment requires PostgreSQL. The application is failing at startup with a `SqlException: Invalid object name 'Roles'`, indicating that the database schema does not exist or the application is connecting to an incorrect/empty database instance. The current `DatabaseInitializer` attempts to migrate, but the existing migrations are likely targeted at SQL Server. To resolve this, the database provider must be switched to PostgreSQL (Npgsql), connection strings updated, and a robust initialization strategy (EnsureDeleted/EnsureCreated for clean slate in dev) implemented to ensure the PostgreSQL schema is correctly generated and seeded.

## Task Plan

### Module: Database Infrastructure

#### Feature: PostgreSQL Migration and Initialization

**T-001: Configure PostgreSQL Provider and Connection Strings**
- **Description:** Update the tech stack to use PostgreSQL instead of SQL Server. This involves modifying `HRMS.UI/Program.cs` to use `UseNpgsql` instead of `UseSqlServer` and updating `appsettings.json` in both `HRMS.UI` and `HRMS.Web` with a valid PostgreSQL connection string.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Program.cs, HRMS.UI/appsettings.json, HRMS.Web/appsettings.json
- **Depends on:** None
- **Acceptance criteria:**
  - `HRMS.UI/Program.cs` uses `options.UseNpgsql(...)`.
  - `appsettings.json` contains a PostgreSQL formatted connection string.
  - The application attempts to connect to PostgreSQL on startup.
- **Wiring:**
  - Imports from: `Npgsql.EntityFrameworkCore.PostgreSQL` (Package)
  - Imported by: None
  - API routes: None
  - DB tables: None
  - Env vars: `ConnectionStrings:DefaultConnection`

**T-002: Implement Robust Database Initialization Logic**
- **Description:** Modify the `DatabaseInitializer` to handle the current "missing tables" issue. Given the inconsistency, the initializer will be updated to use `Database.EnsureDeletedAsync()` followed by `Database.EnsureCreatedAsync()` in the development environment to ensure a clean, correct PostgreSQL schema is created from the current models. This bypasses broken migration history for the transition to PostgreSQL.
- **Files to create:** None
- **Files to modify:** HRMS.Data/Implementations/DatabaseInitializer.cs
- **Depends on:** T-001
- **Acceptance criteria:**
  - `DatabaseInitializer` successfully creates the PostgreSQL schema.
  - No "Invalid object name" errors occur during startup.
  - The initializer logs successful connectivity and creation.
- **Wiring:**
  - Imports from: `HRMS.Data/ApplicationDbContext.cs`
  - Imported by: `HRMS.UI/Program.cs` (via DI)
  - API routes: None
  - DB tables: All (Roles, Users, Employees, etc.)
  - Env vars: None

**T-003: Verify Data Seeding and Connectivity**
- **Description:** Ensure the existing `DataSeeder` runs successfully after the schema is created by T-002. Verify that the `Roles` table is populated, which was the point of failure in the gap report.
- **Files to create:** None
- **Files to modify:** None
- **Depends on:** T-002
- **Acceptance criteria:**
  - Application starts without database errors.
  - `Roles` table exists and contains seeded data (Admin, etc.).
  - User can reach the Login page (as it queries the DB).
- **Wiring:**
  - Imports from: `HRMS.Data/Implementations/DataSeeder.cs`
  - Imported by: `HRMS.Data/Implementations/DatabaseInitializer.cs`
  - API routes: `GET /Account/Login`
  - DB tables: `Roles`, `Users`, `SystemSettings`
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Database Infrastructure",
      "features": [
        {
          "feature": "PostgreSQL Migration and Initialization",
          "tasks": [
            {
              "id": "T-001",
              "name": "Configure PostgreSQL Provider and Connection Strings",
              "description": "Update HRMS.UI/Program.cs to UseNpgsql and update appsettings.json files with PostgreSQL connection strings.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Program.cs",
                "HRMS.UI/appsettings.json",
                "HRMS.Web/appsettings.json"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "HRMS.UI/Program.cs uses options.UseNpgsql(...)",
                "appsettings.json contains a PostgreSQL formatted connection string",
                "The application attempts to connect to PostgreSQL on startup"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "Npgsql.EntityFrameworkCore.PostgreSQL"
                ],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [],
                "env_vars": [
                  "ConnectionStrings:DefaultConnection"
                ]
              }
            },
            {
              "id": "T-002",
              "name": "Implement Robust Database Initialization Logic",
              "description": "Modify DatabaseInitializer.cs to use EnsureDeleted/EnsureCreated for a clean PostgreSQL schema in dev.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.Data/Implementations/DatabaseInitializer.cs"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "DatabaseInitializer successfully creates the PostgreSQL schema",
                "No 'Invalid object name' errors occur during startup",
                "The initializer logs successful connectivity and creation"
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
                  "Roles",
                  "Users",
                  "Employees"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-003",
              "name": "Verify Data Seeding and Connectivity",
              "description": "Ensure DataSeeder runs after schema creation and verify the application starts without DB errors.",
              "files_to_create": [],
              "files_to_modify": [],
              "depends_on": [
                "T-002"
              ],
              "acceptance_criteria": [
                "Application starts without database errors",
                "Roles table exists and contains seeded data",
                "User can reach the Login page"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data/Implementations/DataSeeder.cs"
                ],
                "imported_by": [
                  "HRMS.Data/Implementations/DatabaseInitializer.cs"
                ],
                "api_routes": [
                  "GET /Account/Login"
                ],
                "db_tables": [
                  "Roles",
                  "Users"
                ],
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
