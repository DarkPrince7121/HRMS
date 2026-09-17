# Spec Context — Task 115
**Generated:** 2026-09-02T08:27:25Z  |  **Framework:** KnockoutJS ASP.NET Core PostgreSQL  |  **Tasks:** 2  |  **Project Type:** brownfield

## Gap Analysis Summary
This task introduces the API layer in ASP.NET Core 8.0 for managing the withdrawal applications workflow, active employee metadata, real-time balance calculations, and safe database transactions with concurrency controls. While the database migrations and schema exist for statuses, salary balances, and withdrawal applications, no backend endpoints or controller endpoints exist for these entities yet. We will implement these endpoints inside new dedicated API Controllers (`EmployeesApiController` and `ApplicationsController`), utilizing EF Core transactions for safe, concurrent salary deductions on application submission.

## Task Plan

### Module: Withdrawal Workflow Management

#### Feature: Active Employee Metadata & Calculated Balances

**T-001: Implement Employee API endpoints for metadata and net balance retrieval**
- **Description:** Create the `EmployeesApiController` and required DTOs to retrieve active employees metadata and calculate their real-time net salary balances. The balance calculation will query `EmployeeSalaryBalances` and subtract the sum of any in-progress/pending withdrawal applications (StatusId != 4) associated with the employee.
- **Files to create:**
  - `HRMS.Models/DTOs/EmployeeApiDto.cs`
  - `HRMS.Models/DTOs/EmployeeBalanceDto.cs`
  - `HRMS.UI/Controllers/EmployeesApiController.cs`
- **Files to modify:**
  - `HRMS.UI/Program.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - `GET /api/employees` returns list of active employees including Name (First + Last), Email, Department Name, and BaseSalary with status 200 OK.
  - `GET /api/employees/{id}/balance` retrieves the employee's `AmountBalance` from `EmployeeSalaryBalances` and correctly subtracts the sum of all pending `WithdrawalApplications` (where `StatusId` is 1, 2, or 3) for that employee, returning status 200 OK.
- **Wiring:**
  - Imports from: `HRMS.Data.ApplicationDbContext`, `HRMS.Models.Entities.Employee`, `HRMS.Models.Entities.EmployeeSalaryBalance`, `HRMS.Models.Entities.WithdrawalApplication`
  - Imported by: None (invoked by client-side JavaScript / API consumers)
  - API routes: `GET /api/employees`, `GET /api/employees/{id}/balance`
  - DB tables: `Employees`, `EmployeeSalaryBalances`, `WithdrawalApplications`, `Departments`
  - Env vars: None

---

#### Feature: Withdrawal Application Workflow

**T-002: Implement Withdrawal Application workflow lifecycle and safe completion APIs**
- **Description:** Create the `ApplicationsController` and corresponding status-update DTOs to manage withdrawal application workflows. Implement creation/resume actions, status transition actions, and safe transactional database deductions on completion.
- **Files to create:**
  - `HRMS.Models/DTOs/WithdrawalApplicationDto.cs`
  - `HRMS.Models/DTOs/UpdateStatusDto.cs`
  - `HRMS.UI/Controllers/ApplicationsController.cs`
- **Files to modify:**
  - `HRMS.UI/Program.cs`
- **Depends on:** T-001
- **Acceptance criteria:**
  - `GET /api/applications` returns list of all applications including Status Name, Selected Employee name & email, amount, and relevant metadata timestamps.
  - `POST /api/applications/initiate` starts a new withdrawal application (or resumes an existing in-progress one where `StatusId != 4` for the logged-in user), updates/sets status to 'Application Created' (StatusId = 1), and returns the application ID.
  - `PUT /api/applications/{id}/status` updates status of the application and correctly sets `LastChangeDate` and `StatusLastChangeDate` to UTC now.
  - `POST /api/applications/{id}/complete` updates status to 'Application Submitted' (StatusId = 4), deducts the finalized withdrawal amount from `EmployeeSalaryBalance.AmountBalance` using an explicit EF Core transaction (`BeginTransactionAsync`) to prevent concurrency anomalies, and commits successfully.
- **Wiring:**
  - Imports from: `HRMS.Data.ApplicationDbContext`, `HRMS.Models.Entities.WithdrawalApplication`, `HRMS.Models.Entities.EmployeeSalaryBalance`
  - Imported by: None (invoked by client-side JavaScript / API consumers)
  - API routes: `GET /api/applications`, `POST /api/applications/initiate`, `PUT /api/applications/{id}/status`, `POST /api/applications/{id}/complete`
  - DB tables: `WithdrawalApplications`, `EmployeeSalaryBalances`, `ApplicationStatuses`
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Withdrawal Workflow Management",
      "features": [
        {
          "feature": "Active Employee Metadata & Calculated Balances",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Employee API endpoints for metadata and net balance retrieval",
              "description": "Create the EmployeesApiController and required DTOs to retrieve active employees metadata and calculate their real-time net salary balances. The balance calculation will query EmployeeSalaryBalances and subtract the sum of any in-progress/pending withdrawal applications (StatusId != 4) associated with the employee.",
              "files_to_create": [
                "HRMS.Models/DTOs/EmployeeApiDto.cs",
                "HRMS.Models/DTOs/EmployeeBalanceDto.cs",
                "HRMS.UI/Controllers/EmployeesApiController.cs"
              ],
              "files_to_modify": [
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "GET /api/employees returns list of active employees including Name (First + Last), Email, Department Name, and BaseSalary with status 200 OK.",
                "GET /api/employees/{id}/balance retrieves the employee's AmountBalance from EmployeeSalaryBalances and correctly subtracts the sum of all pending WithdrawalApplications (where StatusId is 1, 2, or 3) for that employee, returning status 200 OK."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data.ApplicationDbContext",
                  "HRMS.Models.Entities.Employee",
                  "HRMS.Models.Entities.EmployeeSalaryBalance",
                  "HRMS.Models.Entities.WithdrawalApplication"
                ],
                "imported_by": [],
                "api_routes": [
                  "GET /api/employees",
                  "GET /api/employees/{id}/balance"
                ],
                "db_tables": [
                  "Employees",
                  "EmployeeSalaryBalances",
                  "WithdrawalApplications",
                  "Departments"
                ],
                "env_vars": []
              }
            }
          ]
        },
        {
          "feature": "Withdrawal Application Workflow",
          "tasks": [
            {
              "id": "T-002",
              "name": "Implement Withdrawal Application workflow lifecycle and safe completion APIs",
              "description": "Create the ApplicationsController and corresponding status-update DTOs to manage withdrawal application workflows. Implement creation/resume actions, status transition actions, and safe transactional database deductions on completion.",
              "files_to_create": [
                "HRMS.Models/DTOs/WithdrawalApplicationDto.cs",
                "HRMS.Models/DTOs/UpdateStatusDto.cs",
                "HRMS.UI/Controllers/ApplicationsController.cs"
              ],
              "files_to_modify": [
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "GET /api/applications returns list of all applications including Status Name, Selected Employee name & email, amount, and relevant metadata timestamps.",
                "POST /api/applications/initiate starts a new withdrawal application (or resumes an existing in-progress one where StatusId != 4 for the logged-in user), updates/sets status to 'Application Created' (StatusId = 1), and returns the application ID.",
                "PUT /api/applications/{id}/status updates status of the application and correctly sets LastChangeDate and StatusLastChangeDate to UTC now.",
                "POST /api/applications/{id}/complete updates status to 'Application Submitted' (StatusId = 4), deducts the finalized withdrawal amount from EmployeeSalaryBalance.AmountBalance using an explicit EF Core transaction (BeginTransactionAsync) to prevent concurrency anomalies, and commits successfully."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data.ApplicationDbContext",
                  "HRMS.Models.Entities.WithdrawalApplication",
                  "HRMS.Models.Entities.EmployeeSalaryBalance"
                ],
                "imported_by": [],
                "api_routes": [
                  "GET /api/applications",
                  "POST /api/applications/initiate",
                  "PUT /api/applications/{id}/status",
                  "POST /api/applications/{id}/complete"
                ],
                "db_tables": [
                  "WithdrawalApplications",
                  "EmployeeSalaryBalances",
                  "ApplicationStatuses"
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
