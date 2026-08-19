# Gap Context — Implement core Employee and Department management modules. Includes Entities, DTOs, Requests/Responses, Services, Controllers, Razor Views, and Knockout ViewModels (save, update, delete, load). UI must use DataTables for listing.

Acceptance Criteria:
- Employee and Department entities created.
- IEmployeeService and IDepartmentService implemented.
- Knockout ViewModels handle CRUD via AJAX.
- DataTables render lists with sorting and paging.

Dependencies: Task auth_implementation
**Date:** 2025-01-24  |  **Task ID:** 74  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core (net6.0)
- **Frontend:** Razor Views, KnockoutJS, DataTables
- **Database:** PostgreSQL (via Npgsql.EntityFrameworkCore.PostgreSQL)
- **Data Access:** Entity Framework Core

### Existing Modules & Features
- **Authentication** (`HRMS.UI/Controllers/AccountController.cs`, `HRMS.UI/wwwroot/js/auth.js`): Basic authentication infrastructure including Login views and logic.
- **Employee Skeleton** (`HRMS.Models/Entities/Employee.cs`, `HRMS.UI/Controllers/EmployeesController.cs`): Minimal entity and controller structure exist but lack the required service layer and full CRUD logic.
- **Data Access Layer** (`HRMS.Data/ApplicationDbContext.cs`): Context is configured for PostgreSQL and includes `Employee` DbSet.

### Prior Context
No prior analysis found for this project.

## Requirements Analysis

### Extracted Requirements
1. **Entity Management:** Implementation of `Employee` and `Department` entities.
2. **Service Layer:** Creation and implementation of `IEmployeeService` and `IDepartmentService`.
3. **Data Transfer Objects:** Implementation of DTOs for Requests and Responses (Employee and Department).
4. **API/Controller Logic:** Controllers to handle CRUD operations, returning JSON for AJAX requests.
5. **Frontend (KnockoutJS):** ViewModels to handle Save, Update, Delete, and Load operations via AJAX.
6. **Frontend (UI):** Razor Views integrated with DataTables for list rendering with sorting and paging.
7. **Database Configuration:** PostgreSQL schema updates for Department and Employee relationship.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Employee Entity | Needs Modification | `HRMS.Models/Entities/Employee.cs` | Needs update to include relationship with Department. |
| Department Entity | New Development | — | Completely missing. |
| IEmployeeService / Implementation | New Development | — | Interfaces and implementations are missing in `HRMS.Services`. |
| IDepartmentService / Implementation | New Development | — | Interfaces and implementations are missing in `HRMS.Services`. |
| DTOs (Request/Response) | New Development | — | Only `LoginRequest.cs` exists; specific CRUD DTOs needed. |
| Controllers (Employee/Dept) | Needs Modification | `HRMS.UI/Controllers/EmployeesController.cs` | Existing controller is skeleton; needs AJAX endpoints and Service injection. |
| Knockout ViewModels | New Development | — | `wwwroot/js/` contains only `auth.js`. |
| Razor Views (DataTables) | Needs Modification | `HRMS.UI/Views/` | `Employees/Index.cshtml` needs implementation with DataTables and KO binding. |

## Tech Stack & Implementation

### Entity & Database Layer — Needs Modification
- **Approach:** Create the `Department` entity and update the `Employee` entity to establish a foreign key relationship. Configure these in `ApplicationDbContext` using Fluent API or Data Annotations.
- **Existing files to modify:** `HRMS.Models/Entities/Employee.cs`, `HRMS.Data/ApplicationDbContext.cs`, `HRMS.Data/Configurations/EmployeeConfiguration.cs`
- **New dependencies:** None

### Service Layer (Business Logic) — New Development
- **Approach:** Define `IEmployeeService` and `IDepartmentService` in the `Interfaces` folder of `HRMS.Services`. Implement them using the repository pattern or direct DbContext access in the `Implementations` folder. Register services for Dependency Injection.
- **Existing files to modify:** `HRMS.UI/Program.cs` (for DI registration)
- **New dependencies:** None

### API & Controller Layer — Needs Modification
- **Approach:** Enhance `EmployeesController` and create `DepartmentsController`. Methods should return `JsonResult` for AJAX-based CRUD and `ViewResult` for the initial page load. Ensure `[Authorize]` attributes are applied.
- **Existing files to modify:** `HRMS.UI/Controllers/EmployeesController.cs`
- **New dependencies:** None

### KnockoutJS & DataTables Integration — New Development
- **Approach:** Create JavaScript files for Employee and Department ViewModels. Use `ko.observableArray` for data and `$.ajax` for server communication. Initialize DataTables on Razor views and bind them to the Knockout data source for paging and sorting.
- **Existing files to modify:** `HRMS.UI/Views/Account/Login.cshtml` (check for layout/script references), `HRMS.UI/wwwroot/js/.gitkeep`
- **New dependencies:** KnockoutJS, jQuery, DataTables (via CDN or local lib)

## Summary
The project is currently a brownfield ASP.NET Core application with a basic authentication framework and some skeleton structures for employees. However, the core business logic (Services), data transfer structures (DTOs), and the specific frontend requirements (KnockoutJS and DataTables) are entirely absent. 

This task involves building out the management core from the bottom up: starting with the database schema for Departments, moving through a service layer to abstract business logic, and ending with a modern-legacy hybrid UI using Razor and KnockoutJS. The implementation is primarily additive, requiring the creation of new architectural layers (Services and DTOs) that are currently missing from the solution structure. Integration with DataTables will be the primary mechanism for list management, requiring a coordinated effort between the JSON-returning controllers and the Knockout ViewModels.
