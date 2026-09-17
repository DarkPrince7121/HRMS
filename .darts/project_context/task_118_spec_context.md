# Spec Context — Task 118
**Generated:** 2026-09-02T09:17:08.180Z  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 3

## Gap Analysis Summary
The project is a brownfield ASP.NET Core web application using KnockoutJS and PostgreSQL. Users trying to navigate to the Applications page or initiate a new withdrawal application encounter errors because the necessary database tables `EmployeeSalaryBalances` and `WithdrawalApplications` do not exist. While two migrations (`20260902073901_AddEmployeeSalaryBalanceTable.cs` and `20260902074721_AddWithdrawalApplicationTable.cs`) were partially written, their corresponding `.Designer.cs` metadata snapshots were never created, and the `ApplicationDbContextModelSnapshot.cs` is out of sync. This prevents Entity Framework Core from applying the schema changes to the PostgreSQL database. Resolving these schema inconsistencies and successfully updating the database will make the existing applications page and creation wizard fully operational.

## Task Plan

### Module: Database Schema & Migrations

#### Feature: EF Core Migration Repair

**T-001: Create missing EF Core migration designer files and align snapshot**
- **Description:** Generate and write the missing `.Designer.cs` files for both pending migrations to provide EF Core with the metadata snapshot required for database updates. Also, update the main model snapshot to include both tables.
- **Files to create:**
  - `HRMS.Data/Migrations/20260902073901_AddEmployeeSalaryBalanceTable.Designer.cs`
  - `HRMS.Data/Migrations/20260902074721_AddWithdrawalApplicationTable.Designer.cs`
- **Files to modify:**
  - `HRMS.Data/Migrations/ApplicationDbContextModelSnapshot.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - `20260902073901_AddEmployeeSalaryBalanceTable.Designer.cs` contains the correct partial class matching the target model build.
  - `20260902074721_AddWithdrawalApplicationTable.Designer.cs` contains the correct partial class matching the target model build.
  - `ApplicationDbContextModelSnapshot.cs` contains entity metadata for `EmployeeSalaryBalance` and `WithdrawalApplication`.
- **Wiring:**
  - Imports from: None
  - Imported by: None
  - API routes: None
  - DB tables: `EmployeeSalaryBalances`, `WithdrawalApplications`
  - Env vars: None

**T-002: Execute database migrations**
- **Description:** Apply the pending migrations to the PostgreSQL database so that the `EmployeeSalaryBalances` and `WithdrawalApplications` tables are fully instantiated in the target database schema.
- **Files to create:** None
- **Files to modify:** None
- **Depends on:** T-001
- **Acceptance criteria:**
  - Database migrations are applied successfully using the dotnet-ef core tool CLI or programmatic initializer.
  - Database tables `EmployeeSalaryBalances` and `WithdrawalApplications` exist in the PostgreSQL database.
- **Wiring:**
  - Imports from: None
  - Imported by: None
  - API routes: None
  - DB tables: `EmployeeSalaryBalances`, `WithdrawalApplications`
  - Env vars: `ConnectionStrings__DefaultConnection` or relevant database connection parameters

### Module: Payroll Applications View

#### Feature: Applications & Wizard Navigation

**T-003: Verify Applications view loading and Wizard navigation**
- **Description:** Verify that navigating to the `/Applications` URL displays the list of withdrawal applications successfully. Verify that clicking the "Create New Withdrawal Application" button correctly launches the 4-step wizard modal.
- **Files to create:** None
- **Files to modify:** None
- **Depends on:** T-002
- **Acceptance criteria:**
  - Navigating to `/Applications` loads without console errors.
  - Clicking "Create New Withdrawal Application" opens the 4-step wizard modal.
  - The API endpoint GET `/api/applications` returns a status 200 OK with the application list.
- **Wiring:**
  - Imports from: `HRMS.UI/Controllers/ApplicationsController.cs`, `HRMS.UI/Views/Applications/Index.cshtml`
  - Imported by: None
  - API routes: `GET /api/applications`, `POST /api/applications/initiate`
  - DB tables: `EmployeeSalaryBalances`, `WithdrawalApplications`, `ApplicationStatuses`, `Employees`
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Database Schema & Migrations",
      "features": [
        {
          "feature": "EF Core Migration Repair",
          "tasks": [
            {
              "id": "T-001",
              "name": "Create missing EF Core migration designer files and align snapshot",
              "description": "Generate and write the missing .Designer.cs files for both pending migrations to provide EF Core with the metadata snapshot required for database updates. Also, update the main model snapshot to include both tables.",
              "files_to_create": [
                "HRMS.Data/Migrations/20260902073901_AddEmployeeSalaryBalanceTable.Designer.cs",
                "HRMS.Data/Migrations/20260902074721_AddWithdrawalApplicationTable.Designer.cs"
              ],
              "files_to_modify": [
                "HRMS.Data/Migrations/ApplicationDbContextModelSnapshot.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "20260902073901_AddEmployeeSalaryBalanceTable.Designer.cs contains the correct partial class matching the target model build.",
                "20260902074721_AddWithdrawalApplicationTable.Designer.cs contains the correct partial class matching the target model build.",
                "ApplicationDbContextModelSnapshot.cs contains entity metadata for EmployeeSalaryBalance and WithdrawalApplication."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [
                  "EmployeeSalaryBalances",
                  "WithdrawalApplications"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-002",
              "name": "Execute database migrations",
              "description": "Apply the pending migrations to the PostgreSQL database so that the EmployeeSalaryBalances and WithdrawalApplications tables are fully instantiated in the target database schema.",
              "files_to_create": [],
              "files_to_modify": [],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "Database migrations are applied successfully using the dotnet-ef core tool CLI or programmatic initializer.",
                "Database tables EmployeeSalaryBalances and WithdrawalApplications exist in the PostgreSQL database."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [],
                "imported_by": [],
                "api_routes": [],
                "db_tables": [
                  "EmployeeSalaryBalances",
                  "WithdrawalApplications"
                ],
                "env_vars": [
                  "ConnectionStrings__DefaultConnection"
                ]
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Payroll Applications View",
      "features": [
        {
          "feature": "Applications & Wizard Navigation",
          "tasks": [
            {
              "id": "T-003",
              "name": "Verify Applications view loading and Wizard navigation",
              "description": "Verify that navigating to the /Applications URL displays the list of withdrawal applications successfully. Verify that clicking the 'Create New Withdrawal Application' button correctly launches the 4-step wizard modal.",
              "files_to_create": [],
              "files_to_modify": [],
              "depends_on": [
                "T-002"
              ],
              "acceptance_criteria": [
                "Navigating to /Applications loads without console errors.",
                "Clicking 'Create New Withdrawal Application' opens the 4-step wizard modal.",
                "The API endpoint GET /api/applications returns a status 200 OK with the application list."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.UI/Controllers/ApplicationsController.cs",
                  "HRMS.UI/Views/Applications/Index.cshtml"
                ],
                "imported_by": [],
                "api_routes": [
                  "GET /api/applications",
                  "POST /api/applications/initiate"
                ],
                "db_tables": [
                  "EmployeeSalaryBalances",
                  "WithdrawalApplications",
                  "ApplicationStatuses",
                  "Employees"
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
