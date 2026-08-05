# Spec Context — Task 86
**Generated:** 2025-05-14  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 3

## Gap Analysis Summary
The HRMS application is in a brownfield state where the database exists but lacks the required tables and seeded data. The primary issue is an infrastructure mismatch: the application is configured to use SQL Server in `Program.cs`, while the target environment and connection string are for PostgreSQL. Additionally, the `AccountController` uses a simple equality check for passwords, whereas the `DataSeeder` stores hashed passwords. This plan fixes the database provider, aligns the authentication logic with the seeding mechanism, and ensures the database is correctly initialized with the schema and admin account.

## Task Plan

### Module: Infrastructure

#### Feature: Database Alignment
**T-001: Configure PostgreSQL Provider and Apply Migrations**
- **Description:** Switch the database provider from SQL Server to PostgreSQL in the startup configuration and project files. This will allow the existing `DatabaseInitializer` to successfully create the schema and seed initial data.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Program.cs, HRMS.Data/HRMS.Data.csproj
- **Depends on:** None
- **Acceptance criteria:**
  - `HRMS.UI/Program.cs` uses `options.UseNpgsql` instead of `options.UseSqlServer`.
  - `HRMS.Data/HRMS.Data.csproj` includes `Npgsql.EntityFrameworkCore.PostgreSQL`.
  - On application startup, all tables (Users, Roles, Employees, etc.) are created in the PostgreSQL database.
- **Wiring:**
  - Imports from: Npgsql.EntityFrameworkCore.PostgreSQL
  - Imported by: None
  - API routes: None
  - DB tables: All domain tables
  - Env vars: ConnectionStrings:DefaultConnection

### Module: Authentication

#### Feature: Login Integrity
**T-002: Update AccountController to support Hashed Passwords**
- **Description:** Modify the login logic to use ASP.NET Core Identity's `PasswordHasher` to compare the incoming plain-text password with the stored hash in the database.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Controllers/AccountController.cs
- **Depends on:** T-001
- **Acceptance criteria:**
  - `AccountController.Login` uses `PasswordHasher<User>.VerifyHashedPassword` for authentication.
  - Login fails for incorrect credentials.
  - Login succeeds for the seeded Admin account (username: 'admin', password: 'Admin@123').
- **Wiring:**
  - Imports from: Microsoft.AspNetCore.Identity, HRMS.Models.Entities
  - Imported by: None
  - API routes: POST /Account/Login
  - DB tables: Users, Roles
  - Env vars: None

**T-003: Finalize and Verify Database Seeding**
- **Description:** Verify that the `DataSeeder` correctly populates the database with roles, the admin user, and system settings after the infrastructure fix.
- **Files to create:** None
- **Files to modify:** None
- **Depends on:** T-002
- **Acceptance criteria:**
  - Roles table contains 'Admin', 'Manager', 'Employee'.
  - Users table contains the 'admin' user linked to the 'Admin' role.
  - SystemSettings table contains default configuration keys.
- **Wiring:**
  - Imports from: None
  - Imported by: None
  - API routes: None
  - DB tables: Roles, Users, SystemSettings, Departments, Designations
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Infrastructure",
      "features": [
        {
          "feature": "Database Alignment",
          "tasks": [
            {
              "id": "T-001",
              "name": "Configure PostgreSQL Provider and Apply Migrations",
              "description": "Switch the database provider from SQL Server to PostgreSQL in the startup configuration and project files.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Program.cs",
                "HRMS.Data/HRMS.Data.csproj"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "HRMS.UI/Program.cs uses options.UseNpgsql instead of options.UseSqlServer.",
                "HRMS.Data/HRMS.Data.csproj includes Npgsql.EntityFrameworkCore.PostgreSQL.",
                "On application startup, all tables are created in PostgreSQL."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "Npgsql.EntityFrameworkCore.PostgreSQL"
                ],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [
                  "Users",
                  "Roles",
                  "Employees",
                  "Attendance",
                  "Leaves",
                  "Payroll",
                  "Departments",
                  "Designations",
                  "SystemSettings"
                ],
                "env_vars": [
                  "ConnectionStrings:DefaultConnection"
                ]
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Authentication",
      "features": [
        {
          "feature": "Login Integrity",
          "tasks": [
            {
              "id": "T-002",
              "name": "Update AccountController to support Hashed Passwords",
              "description": "Modify the login logic to use PasswordHasher to compare plain-text passwords with stored hashes.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Controllers/AccountController.cs"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "AccountController uses PasswordHasher for verification.",
                "Seeded admin account can successfully log in."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "Microsoft.AspNetCore.Identity",
                  "HRMS.Models.Entities"
                ],
                "imported_by": [],
                "api_routes": [
                  "POST /Account/Login"
                ],
                "db_tables": [
                  "Users",
                  "Roles"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-003",
              "name": "Finalize and Verify Database Seeding",
              "description": "Verify that the DataSeeder correctly populates the database after the infrastructure fix.",
              "files_to_create": [],
              "files_to_modify": [],
              "depends_on": [
                "T-002"
              ],
              "acceptance_criteria": [
                "Roles, Users, and SystemSettings are correctly seeded.",
                "Departments and Designations are present in the DB."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [
                  "Roles",
                  "Users",
                  "SystemSettings",
                  "Departments",
                  "Designations"
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