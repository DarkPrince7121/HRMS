# Spec Context — Task 76
**Generated:** 2025-05-14  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 4

## Gap Analysis Summary
The project is a brownfield ASP.NET Core application with a PostgreSQL backend (EF Core) and a KnockoutJS frontend. Task 76 requires the implementation of a Payroll processing module, building upon existing Employee and Attendance/Leave management (Task 75). The core needs are: extending the Employee model with salary configuration, creating a Payroll entity to store monthly records, developing a service for net pay calculation logic (gross - deductions), and providing a UI to trigger and view payroll. The implementation follows the established Service/Controller/Knockout pattern.

## Task Plan

### Module: Payroll Management

#### Feature: Payroll Processing

**T-001: Implement Payroll Data Models and Database Schema**
- **Description:** Extend the `Employee` model to include salary configuration fields and create the `Payroll` entity. Update the `ApplicationDbContext` and add EF Core configurations.
- **Files to create:** `HRMS.Models/Entities/Payroll.cs`, `HRMS.Data/Configurations/PayrollConfiguration.cs`
- **Files to modify:** `HRMS.Models/Entities/Employee.cs`, `HRMS.Data/ApplicationDbContext.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - `Employee` entity contains `BaseSalary` (decimal).
  - `Payroll` entity contains `Id`, `EmployeeId`, `PayDate`, `GrossPay`, `Deductions`, `NetPay`, and `Status`.
  - `ApplicationDbContext` includes `DbSet<Payroll>`.
  - Database migration can be generated successfully.
- **Wiring:**
  - Imports from: `HRMS.Models.Entities`
  - Imported by: `HRMS.Data.ApplicationDbContext`, `HRMS.Services.PayrollService`
  - API routes: None
  - DB tables: `Employees` (modified), `Payrolls` (new)
  - Env vars: None

**T-002: Implement Payroll Service and Calculation Logic**
- **Description:** Create `IPayrollService` and `PayrollService` to handle payroll generation and net pay calculation. The calculation should retrieve employee base salary and apply logic (e.g., deducting based on leave data from Task 75).
- **Files to create:** `HRMS.Services/Interfaces/IPayrollService.cs`, `HRMS.Services/Implementations/PayrollService.cs`
- **Files to modify:** `HRMS.UI/Program.cs`
- **Depends on:** T-001
- **Acceptance criteria:**
  - `CalculateNetPay(int employeeId, DateTime month)` correctly subtracts deductions from base salary.
  - `GeneratePayrollForMonth(DateTime month)` creates records for all active employees.
  - Service is registered in `Program.cs`.
- **Wiring:**
  - Imports from: `HRMS.Models.Entities`, `HRMS.Data`, `HRMS.Services.Interfaces`
  - Imported by: `HRMS.UI.Controllers.PayrollController`
  - API routes: None
  - DB tables: `Employees`, `Payrolls`, `Leaves`, `Attendances`
  - Env vars: None

**T-003: Implement Payroll API Controller and DTOs**
- **Description:** Create `PayrollController` to expose endpoints for generating and retrieving payroll data. Define request/response DTOs for data transfer.
- **Files to create:** `HRMS.UI/Controllers/PayrollController.cs`, `HRMS.Models/DTOs/PayrollResponse.cs`, `HRMS.Models/DTOs/PayrollRequest.cs`
- **Files to modify:** None
- **Depends on:** T-002
- **Acceptance criteria:**
  - `GET /api/payroll` returns list of payroll records.
  - `POST /api/payroll/generate` triggers payroll calculation for a specified month.
  - Returns 200 OK on successful generation.
- **Wiring:**
  - Imports from: `HRMS.Services.Interfaces`, `HRMS.Models.DTOs`
  - Imported by: `HRMS.UI.wwwroot.js.payroll.js`
  - API routes: `GET /api/payroll`, `POST /api/payroll/generate`
  - DB tables: `Payrolls`
  - Env vars: None

**T-004: Implement Payroll UI with KnockoutJS**
- **Description:** Create the Payroll management view using Razor and KnockoutJS. Add navigation link to the main layout.
- **Files to create:** `HRMS.UI/Views/Payroll/Index.cshtml`, `HRMS.UI/wwwroot/js/payroll.js`
- **Files to modify:** `HRMS.UI/Views/Shared/_Layout.cshtml`
- **Depends on:** T-003
- **Acceptance criteria:**
  - Payroll page is accessible via the navigation bar.
  - Page displays a table of payroll records using DataTables.
  - "Generate Payroll" button triggers the API call and refreshes the table.
- **Wiring:**
  - Imports from: `payroll.js` (script tag in Index.cshtml)
  - Imported by: `_Layout.cshtml` (navigation link)
  - API routes: `GET /api/payroll`, `POST /api/payroll/generate`
  - DB tables: None (via API)
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Payroll Management",
      "features": [
        {
          "feature": "Payroll Processing",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Payroll Data Models and Database Schema",
              "description": "Extend the Employee model to include salary configuration fields and create the Payroll entity. Update the ApplicationDbContext and add EF Core configurations.",
              "files_to_create": [
                "HRMS.Models/Entities/Payroll.cs",
                "HRMS.Data/Configurations/PayrollConfiguration.cs"
              ],
              "files_to_modify": [
                "HRMS.Models/Entities/Employee.cs",
                "HRMS.Data/ApplicationDbContext.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Employee entity contains BaseSalary (decimal).",
                "Payroll entity contains Id, EmployeeId, PayDate, GrossPay, Deductions, NetPay, and Status.",
                "ApplicationDbContext includes DbSet<Payroll>.",
                "Database migration can be generated successfully."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models.Entities"
                ],
                "imported_by": [
                  "HRMS.Data.ApplicationDbContext",
                  "HRMS.Services.PayrollService"
                ],
                "api_routes": [],
                "db_tables": [
                  "Employees",
                  "Payrolls"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-002",
              "name": "Implement Payroll Service and Calculation Logic",
              "description": "Create IPayrollService and PayrollService to handle payroll generation and net pay calculation. The calculation should retrieve employee base salary and apply logic (e.g., deducting based on leave data from Task 75).",
              "files_to_create": [
                "HRMS.Services/Interfaces/IPayrollService.cs",
                "HRMS.Services/Implementations/PayrollService.cs"
              ],
              "files_to_modify": [
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "CalculateNetPay(int employeeId, DateTime month) correctly subtracts deductions from base salary.",
                "GeneratePayrollForMonth(DateTime month) creates records for all active employees.",
                "Service is registered in Program.cs."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models.Entities",
                  "HRMS.Data",
                  "HRMS.Services.Interfaces"
                ],
                "imported_by": [
                  "HRMS.UI.Controllers.PayrollController"
                ],
                "api_routes": [],
                "db_tables": [
                  "Employees",
                  "Payrolls",
                  "Leaves",
                  "Attendances"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-003",
              "name": "Implement Payroll API Controller and DTOs",
              "description": "Create PayrollController to expose endpoints for generating and retrieving payroll data. Define request/response DTOs for data transfer.",
              "files_to_create": [
                "HRMS.UI/Controllers/PayrollController.cs",
                "HRMS.Models/DTOs/PayrollResponse.cs",
                "HRMS.Models/DTOs/PayrollRequest.cs"
              ],
              "files_to_modify": [],
              "depends_on": [
                "T-002"
              ],
              "acceptance_criteria": [
                "GET /api/payroll returns list of payroll records.",
                "POST /api/payroll/generate triggers payroll calculation for a specified month.",
                "Returns 200 OK on successful generation."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Services.Interfaces",
                  "HRMS.Models.DTOs"
                ],
                "imported_by": [
                  "HRMS.UI.wwwroot.js.payroll.js"
                ],
                "api_routes": [
                  "GET /api/payroll",
                  "POST /api/payroll/generate"
                ],
                "db_tables": [
                  "Payrolls"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-004",
              "name": "Implement Payroll UI with KnockoutJS",
              "description": "Create the Payroll management view using Razor and KnockoutJS. Add navigation link to the main layout.",
              "files_to_create": [
                "HRMS.UI/Views/Payroll/Index.cshtml",
                "HRMS.UI/wwwroot/js/payroll.js"
              ],
              "files_to_modify": [
                "HRMS.UI/Views/Shared/_Layout.cshtml"
              ],
              "depends_on": [
                "T-003"
              ],
              "acceptance_criteria": [
                "Payroll page is accessible via the navigation bar.",
                "Page displays a table of payroll records using DataTables.",
                "Generate Payroll button triggers the API call and refreshes the table."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "payroll.js"
                ],
                "imported_by": [
                  "_Layout.cshtml"
                ],
                "api_routes": [
                  "GET /api/payroll",
                  "POST /api/payroll/generate"
                ],
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