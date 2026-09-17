# Gap Context — Build backend controllers and APIs in ASP.NET to manage the withdrawal applications workflow, retrieve active employee metadata, fetch real-time calculated balances, and finalize application transactions with safe database deductions.

Acceptance Criteria:
- GET /api/applications endpoint returns list of all applications with status names, target employee info, amount, and relevant metadata timestamps
- POST /api/applications/initiate starts an application (or resumes existing in-progress one), updating status to 'Application Created' (StatusId matching its value) and returns application ID
- PUT /api/applications/{id}/status updates the status (e.g. to 'User Selected', 'Amount Entered', or 'Application Submitted') and updates LastChangeDate and StatusLastChangeDate
- GET /api/employees list returns all active employees with Name, Email, Department, and Salary information
- GET /api/employees/{id}/balance returns Employee's processed payroll balance minus any pending withdrawal application amounts
- POST /api/applications/{id}/complete updates application status to 'Application Submitted', subtracts the finalized withdrawal amount from EmployeeSalaryBalance, and saves transaction

Technical Hints: Use transactions or safe EF Core updates during '/complete' invocation to avoid concurrency conflicts when updating EmployeeSalaryBalance. Ensure to deduct the application's Amount from EmployeeSalaryBalance inside a unified transaction.

Dependencies: Task db-migration-withdrawal-application, Task db-migration-employee-salary-balance

**Date:** 2026-09-02  |  **Task ID:** 115  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 8.0, Entity Framework Core (EF Core)
- **Database:** PostgreSQL (using Npgsql Provider)
- **Frontend:** KnockoutJS, HTML, Bootstrap/CSS (managed in ASP.NET MVC Views)

### Existing Modules & Features
- **Database Context** (`HRMS.Data/ApplicationDbContext.cs`): Manages DbSets and EF configurations for models including `Employees`, `ApplicationStatuses`, `EmployeeSalaryBalances`, and `WithdrawalApplications`.
- **Employee Services** (`HRMS.Services/EmployeeService.cs`): Handles employee retrieval, creation, modification, and deletion.
- **Employee Controllers** (`HRMS.UI/Controllers/EmployeesController.cs`): Contains MVC actions for employee view and standard json lookup endpoints.
- **Application Services & Configuration** (`HRMS.UI/Program.cs`): Configures MVC controllers, routes, cookie authentication, DbContext integration, and registers application services.

### Prior Context
No prior task-specific analysis found for this project context, but the codebase has recently undergone database migrations (`20260902073628_AddApplicationStatusTable`, `20260902073901_AddEmployeeSalaryBalanceTable`, and `20260902074721_AddWithdrawalApplicationTable`) laying out schema tables for application processing, withdrawal statuses, and employee salary balances.

## Requirements Analysis

### Extracted Requirements
1. **GET `/api/applications`**: Return all applications along with status names, target employee name/email, amount, and metadata timestamps.
2. **POST `/api/applications/initiate`**: Start a new withdrawal application (or resume an existing in-progress one), transition/set status to "Application Created", and return the application ID.
3. **PUT `/api/applications/{id}/status`**: Accept status updates (e.g. "User Selected", "Amount Entered", "Application Submitted"), updating the status, `LastChangeDate`, and `StatusLastChangeDate`.
4. **GET `/api/employees`**: Return a list of active employees including Name (FirstName, LastName), Email, Department, and Salary details.
5. **GET `/api/employees/{id}/balance`**: Return Employee's real-time salary balance (`EmployeeSalaryBalance.AmountBalance`) minus any currently pending/active withdrawal applications' amounts.
6. **POST `/api/applications/{id}/complete`**: Finalize an application by updating its status to "Application Submitted", safely deducting the finalized amount from `EmployeeSalaryBalance.AmountBalance` inside a unified EF Core transaction to avoid concurrency conflicts, and committing changes.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| GET `/api/applications` | New Development | `HRMS.UI/Controllers/` / `HRMS.UI/Program.cs` | Requires new controller or endpoint configuration to query `WithdrawalApplications` and map metadata fields. |
| POST `/api/applications/initiate` | New Development | `HRMS.UI/Controllers/` / `HRMS.UI/Program.cs` | Requires route handler to initiate/resume application workflow and track statuses. |
| PUT `/api/applications/{id}/status` | New Development | `HRMS.UI/Controllers/` / `HRMS.UI/Program.cs` | Requires status state transition logic with updated timestamp tracking. |
| GET `/api/employees` | Needs Modification | `HRMS.UI/Controllers/EmployeesController.cs` | Already has an MVC search list endpoint but needs a formal `/api/employees` representation returning exact requested fields. |
| GET `/api/employees/{id}/balance` | New Development | `HRMS.UI/Controllers/` / `HRMS.UI/Program.cs` | Requires query implementation that retrieves processed balance and subtracts pending application totals. |
| POST `/api/applications/{id}/complete` | New Development | `HRMS.UI/Controllers/` / `HRMS.UI/Program.cs` | Requires transactional update block (using `DbContext.Database.BeginTransactionAsync()` or safe concurrent row locking) to prevent race-conditions. |

## Tech Stack & Implementation

### Withdrawal Application APIs & Workflow — New Development
- **Approach:** 
  Create REST API handlers supporting HTTP GET, POST, and PUT operations for `/api/applications`. The controller will interact with `ApplicationDbContext` directly or via dedicated service methods to register applications, transition through workflow states (StatusId), and update metadata timestamps (`LastChangeDate`, `StatusLastChangeDate`).
- **Existing files to modify:** 
  - `HRMS.UI/Program.cs` (to verify configuration & map APIs)
- **New dependencies:** None

### Employee Active Metadata & Calculated Balances — Needs Modification
- **Approach:** 
  Implement an endpoint `/api/employees` returning active employees with Name, Email, Department, and BaseSalary details. For `/api/employees/{id}/balance`, fetch the employee's base balance from `EmployeeSalaryBalances` and subtract the sum of `Amount` from all pending `WithdrawalApplications` (applications that are in-progress or initiated but not finalized/completed) to yield the real-time net balance.
- **Existing files to modify:** 
  - `HRMS.UI/Controllers/EmployeesController.cs` (to introduce `/api/employees` list and balance retrieval endpoint)
- **New dependencies:** None

### Safe Database Deductions & Completion — New Development
- **Approach:** 
  Implement POST `/api/applications/{id}/complete` wrapped inside a database-backed transaction using `await using var transaction = await _context.Database.BeginTransactionAsync()`. Within the transaction, fetch the current employee's salary balance row with an explicit read lock or perform atomic updates on `EmployeeSalaryBalances` to prevent concurrency problems, reduce the balance by the final application `Amount`, mark the application status as "Application Submitted", and commit the transaction safely.
- **Existing files to modify:** 
  - `HRMS.UI/Program.cs`
- **New dependencies:** None

## Summary
The HRMS project is an ASP.NET Core and KnockoutJS application connected to a PostgreSQL database. The application's database schema already defines tables for employee management, payroll, withdrawal applications, and salary balances, indicating a mature Brownfield codebase. However, there are no existing backend controllers or operational services managing the lifecycle of withdrawal applications or processing transactional salary balance deductions.

This task introduces the core REST API layer enabling users to query active employees, calculate net balances (including deductions for pending applications), start/modify withdrawal workflow applications, and finalize completed applications. The implementation will require modifying existing controller definitions to add API routes or introducing new endpoint routing, and applying EF Core transactions during withdrawal completion to ensure strict concurrency control and database consistency.
