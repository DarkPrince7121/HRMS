# Gap Context — Develop Attendance and Leave management modules following the established service/view/viewModel pattern. Includes tracking, status updates, and reporting.

Acceptance Criteria:
- Attendance and Leave entities created.
- AJAX-based submission for leave requests and attendance marking.

Dependencies: Task core_employee_dept_modules_crud
**Date:** 2025-05-14  |  **Task ID:** 75  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core (likely .NET 6/7/8 based on top-level statements in Program.cs and file structure)
- **Database:** PostgreSQL (with Entity Framework Core)
- **Frontend:** KnockoutJS, jQuery, DataTables, Bootstrap
- **Architecture:** Service/View/ViewModel pattern with a clean separation between Data, Models, Services, and UI layers.

### Existing Modules & Features
- **Employee Management** (`HRMS.UI/Controllers/EmployeesController.cs`, `HRMS.UI/wwwroot/js/employees.js`): CRUD operations for employees using KnockoutJS and AJAX.
- **Department Management** (`HRMS.UI/Controllers/DepartmentsController.cs`, `HRMS.UI/wwwroot/js/departments.js`): CRUD operations for departments.
- **Authentication** (`HRMS.UI/Controllers/AccountController.cs`, `HRMS.UI/wwwroot/js/auth.js`): User login and role management.
- **Data Layer** (`HRMS.Data/`): EF Core context and configurations for PostgreSQL.

### Prior Context
No prior analysis found for this project.

## Requirements Analysis

### Extracted Requirements
1. **Attendance Tracking:** Create entities and logic to record employee attendance.
2. **Leave Management:** Create entities and logic to request and track leaves.
3. **Service/View/ViewModel Pattern:** Adhere to the existing architectural pattern for both modules.
4. **Status Updates:** Implement status transitions (e.g., Pending, Approved, Rejected for Leaves).
5. **Reporting:** Provide summarized views or data for attendance and leave records.
6. **AJAX Submissions:** Use asynchronous requests for marking attendance and submitting leave requests, consistent with `employees.js`.
7. **Database Schema:** Extend the PostgreSQL schema via EF Core migrations to include Attendance and Leave tables.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Attendance Entity | New Development | `HRMS.Models/Entities/` | Needs new class and DB configuration. |
| Leave Entity | New Development | `HRMS.Models/Entities/` | Needs new class and DB configuration. |
| Attendance Logic | New Development | `HRMS.Services/`, `HRMS.UI/Controllers/` | Requires new Service and Controller. |
| Leave Logic | New Development | `HRMS.Services/`, `HRMS.UI/Controllers/` | Requires new Service and Controller. |
| AJAX Submission | New Development | `HRMS.UI/wwwroot/js/` | New JS files following `employees.js` pattern. |
| Status Updates | New Development | — | Business logic in Services for Leave approvals. |
| Reporting | New Development | — | DataTables-based views for summary data. |

## Tech Stack & Implementation

### Attendance & Leave Entities — New Development
- **Approach:** Create POCO classes in the Models project. Define relationships (e.g., `EmployeeId` foreign keys). Configure the mapping in the Data project using `IEntityTypeConfiguration` and update `ApplicationDbContext`.
- **Existing files to modify:** `HRMS.Data/ApplicationDbContext.cs`
- **New dependencies:** None

### Attendance & Leave Services — New Development
- **Approach:** Implement the business logic (marking attendance, validating leave dates, updating status) in new service classes. Define interfaces in `HRMS.Services/Interfaces` and implementations in `HRMS.Services/`. Register these in the DI container.
- **Existing files to modify:** `HRMS.UI/Program.cs` (for DI registration)
- **New dependencies:** None

### KnockoutJS ViewModels — New Development
- **Approach:** Create new JavaScript files in `wwwroot/js/`. Implement ViewModels using the pattern observed in `employees.js`, utilizing `ko.observable`, `ko.observableArray`, and jQuery `$.ajax` for server communication.
- **Existing files to modify:** None
- **New dependencies:** None

### UI Views — New Development
- **Approach:** Create new Razor Views in `HRMS.UI/Views/`. Use Bootstrap for layouts and DataTables for displaying history/reports, consistent with `Employees/Index.cshtml`.
- **Existing files to modify:** `HRMS.UI/Views/Shared/_Layout.cshtml` (to add navigation links)
- **New dependencies:** None

## Summary
The project is a mature ASP.NET Core application with a clear separation of concerns and a standard KnockoutJS-based frontend pattern. The infrastructure for data access (PostgreSQL via EF Core) and frontend interaction (AJAX/Knockout) is well-established.

This task requires the addition of two core HR modules: Attendance and Leave management. The implementation will be largely additive, following the existing "Service/View/ViewModel" pattern. This involves extending the database schema, creating backend services for business logic, and developing interactive frontend components. The overall character of the task is "building within an established framework," ensuring consistency with the existing CRUD modules while adding specific workflow logic for leave approvals and attendance tracking.