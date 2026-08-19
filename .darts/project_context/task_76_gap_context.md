# Gap Context — Implement Payroll processing module. Includes salary configuration, payroll generation logic in Services, and calculation models.

Acceptance Criteria:
- Payroll entity and Service created.
- Service method to calculate net pay.

Dependencies: Task attendance_leave_management_mvp
**Date:** 2025-05-14  |  **Task ID:** 76  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core (target Framework context indicates ASP.NET)
- **Database:** PostgreSQL (with EF Core observed in project structure)
- **Frontend:** KnockoutJS (observed in `wwwroot/js` and requirement)
- **Architecture:** Multi-project solution with separation into Data, Models, Services, and UI layers.

### Existing Modules & Features
- **Employee Management** (`HRMS.Models/Entities/Employee.cs`, `HRMS.Services/EmployeeService.cs`): Core employee data handling.
- **Department Management** (`HRMS.Models/Entities/Department.cs`, `HRMS.Services/DepartmentService.cs`): Department organization.
- **Data Access** (`HRMS.Data/ApplicationDbContext.cs`): Entity Framework Core context for PostgreSQL.
- **Frontend Infrastructure** (`HRMS.UI/wwwroot/js/`): KnockoutJS-based ViewModels for AJAX interaction.

### Prior Context
Analysis from Task 75 (`task_75_gap_context.md`) confirms the project follows a Service/View/ViewModel pattern. Task 75 introduced Attendance and Leave management, which are listed as dependencies for this task (likely for calculating deductions or working days).

## Requirements Analysis

### Extracted Requirements
1. **Salary Configuration:** Ability to define base salaries or pay structures (implied by "salary configuration").
2. **Payroll Entity:** Create a model to store payroll records (gross pay, net pay, date, etc.).
3. **Calculation Logic:** Implementation of net pay calculation logic within a Service.
4. **Payroll Generation:** Logic to process payroll for employees.
5. **Data Persistence:** Store payroll information in the PostgreSQL database.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Payroll Entity | New Development | `HRMS.Models/Entities/` | New entity class required. |
| Salary Configuration | New Development | `HRMS.Models/Entities/` | Likely needs additions to `Employee` or a new `SalaryConfig` entity. |
| Payroll Service | New Development | `HRMS.Services/` | New service for business logic. |
| Net Pay Calculation | New Development | `HRMS.Services/` | Method within the new Payroll Service. |
| Database Integration | Needs Modification | `HRMS.Data/ApplicationDbContext.cs` | Add `DbSet` for Payroll and configuration. |
| DI Registration | Needs Modification | `HRMS.UI/Program.cs` | Register the new Payroll service. |

## Tech Stack & Implementation

### Payroll Entity & Configuration — New Development / Needs Modification
- **Approach:** Define a `Payroll` entity in the Models project to store results. Since "salary configuration" is required, the `Employee` entity likely needs extension (e.g., `BaseSalary`) or a related `SalaryConfiguration` entity must be created. EF Core fluent API in the Data project will be used to map these to PostgreSQL.
- **Existing files to modify:** `HRMS.Models/Entities/Employee.cs` (if adding base salary directly), `HRMS.Data/ApplicationDbContext.cs`
- **New dependencies:** None

### Payroll Service & Calculation Logic — New Development
- **Approach:** Create a `PayrollService` implementing an `IPayrollService` interface. The calculation logic for net pay should consider base salary (configuration) and potentially data from the dependency task (attendance/leaves) for deductions. This follows the existing service pattern.
- **Existing files to modify:** `HRMS.UI/Program.cs` (for service registration)
- **New dependencies:** None

### UI / Generation Trigger — New Development
- **Approach:** Although not explicitly in AC, "Payroll generation" implies a trigger. This would involve a new KnockoutJS ViewModel and a Razor View, following the pattern in `employees.js`.
- **Existing files to modify:** `HRMS.UI/Views/Shared/_Layout.cshtml` (for navigation)
- **New dependencies:** None

## Summary
The project is a structured ASP.NET Core application using KnockoutJS for the frontend and PostgreSQL for data storage. It has established patterns for entity configuration, service-based business logic, and AJAX-driven UI.

This task involves building the Payroll module. This is a primarily additive task that extends the system's business logic. It requires defining how salary is configured (either on the employee or a new config entity), implementing the math for net pay calculations within a new Service, and creating the storage schema for payroll runs. The implementation must integrate with the previously developed Attendance and Leave modules to accurately calculate pay based on work records. It will follow the existing architectural pattern: Entity -> DbContext -> Service -> Controller -> Knockout ViewModel.