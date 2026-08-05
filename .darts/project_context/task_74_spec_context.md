# Spec Context — Task 74
**Generated:** 2025-01-24  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 4

## Gap Analysis Summary
This task involves expanding a brownfield ASP.NET Core HRMS application to include core Employee and Department management. The existing state includes a basic authentication skeleton and a minimal `Employee` entity. We need to implement a full vertical slice for both `Department` (entirely new) and `Employee` (needs significant updates). This includes database schema changes (PostgreSQL), a service layer with Dependency Injection, JSON-based CRUD controllers, and a KnockoutJS-powered frontend integrated with DataTables for list management.

## Task Plan

### Module: Core HR Management

#### Feature: Department Management

**T-001: Implement Department Management — Backend and Frontend**
- **Description:** Implement the full vertical slice for Department management. This includes creating the `Department` entity, `DepartmentRequest`/`DepartmentResponse` DTOs, `IDepartmentService`, and `DepartmentsController` with AJAX endpoints. On the frontend, create a Razor view with a DataTables list and a KnockoutJS ViewModel to handle CRUD operations.
- **Files to create:** `HRMS.Models/Entities/Department.cs`, `HRMS.Models/DTOs/DepartmentRequest.cs`, `HRMS.Models/DTOs/DepartmentResponse.cs`, `HRMS.Services/Interfaces/IDepartmentService.cs`, `HRMS.Services/Implementations/DepartmentService.cs`, `HRMS.UI/Controllers/DepartmentsController.cs`, `HRMS.UI/Views/Departments/Index.cshtml`, `HRMS.UI/wwwroot/js/departments.js`
- **Files to modify:** `HRMS.Data/ApplicationDbContext.cs`, `HRMS.UI/Program.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - `Department` entity created and mapped in `ApplicationDbContext`.
  - `IDepartmentService` registered in `Program.cs`.
  - `GET /Departments/GetAll` returns JSON list of departments.
  - `POST /Departments/Create` correctly persists a new department.
  - `Index.cshtml` renders a DataTable with sorting and paging.
  - Knockout ViewModel handles department creation and deletion via AJAX.
- **Wiring:**
  - Imports from: `ApplicationDbContext`, `IDepartmentService`
  - Imported by: `Program.cs` (DI registration)
  - API routes: `GET /Departments`, `GET /Departments/GetAll`, `POST /Departments/Create`, `POST /Departments/Edit`, `POST /Departments/Delete/{id}`
  - DB tables: `Departments`
  - Env vars: `DefaultConnection` (existing)

#### Feature: Employee Management

**T-002: Update Employee Entity and Create Service Layer**
- **Description:** Update the existing `Employee` entity to include a relationship with `Department`. Create `EmployeeRequest` and `EmployeeResponse` DTOs. Implement `IEmployeeService` and its implementation to handle business logic and database operations, moving logic away from the controller skeleton.
- **Files to create:** `HRMS.Models/DTOs/EmployeeRequest.cs`, `HRMS.Models/DTOs/EmployeeResponse.cs`, `HRMS.Services/Interfaces/IEmployeeService.cs`, `HRMS.Services/Implementations/EmployeeService.cs`
- **Files to modify:** `HRMS.Models/Entities/Employee.cs`, `HRMS.UI/Program.cs`, `HRMS.Data/Configurations/EmployeeConfiguration.cs`
- **Depends on:** T-001
- **Acceptance criteria:**
  - `Employee` entity has a `DepartmentId` and `Department` navigation property.
  - `EmployeeConfiguration` updated for the relationship.
  - `IEmployeeService` handles CRUD logic for Employees.
- **Wiring:**
  - Imports from: `Employee.cs`, `Department.cs`, `ApplicationDbContext`
  - Imported by: `Program.cs`, `EmployeesController.cs`
  - API routes: None
  - DB tables: `Employees`, `Departments`
  - Env vars: None

**T-003: Implement Employee AJAX Controller Endpoints**
- **Description:** Enhance the existing `EmployeesController` to use `IEmployeeService`. Implement JSON-returning endpoints for DataTables and KnockoutJS integration (GetAll, Create, Edit, Delete).
- **Files to create:** None
- **Files to modify:** `HRMS.UI/Controllers/EmployeesController.cs`
- **Depends on:** T-002
- **Acceptance criteria:**
  - Controller methods injected with `IEmployeeService`.
  - `GET /Employees/GetAll` returns JSON formatted for DataTables.
  - CRUD operations return `JsonResult`.
- **Wiring:**
  - Imports from: `IEmployeeService`, `EmployeeRequest`, `EmployeeResponse`
  - Imported by: `HRMS.UI/Views/Employees/Index.cshtml` (via AJAX)
  - API routes: `GET /Employees/GetAll`, `POST /Employees/Create`, `POST /Employees/Edit`, `POST /Employees/Delete/{id}`
  - DB tables: `Employees`
  - Env vars: None

**T-004: Implement Employee UI with KnockoutJS and DataTables**
- **Description:** Create the frontend implementation for Employee management. This includes the `EmployeeViewModel` in KnockoutJS and the Razor view that integrates DataTables. The UI should support loading, adding, editing, and deleting employees with a department dropdown.
- **Files to create:** `HRMS.UI/Views/Employees/Index.cshtml`, `HRMS.UI/wwwroot/js/employees.js`
- **Files to modify:** None
- **Depends on:** T-003
- **Acceptance criteria:**
  - `Index.cshtml` displays a DataTable of employees.
  - `employees.js` contains a Knockout ViewModel managing the state.
  - Create/Edit forms correctly populate department options (via AJAX call to Departments).
  - Delete operation refreshes the DataTable without full page reload.
- **Wiring:**
  - Imports from: `jQuery`, `KnockoutJS`, `DataTables`
  - Imported by: `Layout.cshtml` (via script sections)
  - API routes: `GET /Employees/GetAll`, `GET /Departments/GetAll`
  - DB tables: None (Client side)
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Core HR Management",
      "features": [
        {
          "feature": "Department Management",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Department Management — Backend and Frontend",
              "description": "Implement full vertical slice for Department management including entity, DTOs, service, controller, and UI with Knockout and DataTables.",
              "files_to_create": [
                "HRMS.Models/Entities/Department.cs",
                "HRMS.Models/DTOs/DepartmentRequest.cs",
                "HRMS.Models/DTOs/DepartmentResponse.cs",
                "HRMS.Services/Interfaces/IDepartmentService.cs",
                "HRMS.Services/Implementations/DepartmentService.cs",
                "HRMS.UI/Controllers/DepartmentsController.cs",
                "HRMS.UI/Views/Departments/Index.cshtml",
                "HRMS.UI/wwwroot/js/departments.js"
              ],
              "files_to_modify": [
                "HRMS.Data/ApplicationDbContext.cs",
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Department entity created and mapped in ApplicationDbContext",
                "IDepartmentService registered in Program.cs",
                "GET /Departments/GetAll returns JSON list",
                "Knockout ViewModel handles department CRUD via AJAX",
                "Index view renders DataTable"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["ApplicationDbContext", "IDepartmentService"],
                "imported_by": ["Program.cs"],
                "api_routes": ["GET /Departments/GetAll", "POST /Departments/Create", "POST /Departments/Delete/{id}"],
                "db_tables": ["Departments"],
                "env_vars": ["DefaultConnection"]
              }
            }
          ]
        },
        {
          "feature": "Employee Management",
          "tasks": [
            {
              "id": "T-002",
              "name": "Update Employee Entity and Create Service Layer",
              "description": "Update Employee entity with Department relationship and implement IEmployeeService with DTOs.",
              "files_to_create": [
                "HRMS.Models/DTOs/EmployeeRequest.cs",
                "HRMS.Models/DTOs/EmployeeResponse.cs",
                "HRMS.Services/Interfaces/IEmployeeService.cs",
                "HRMS.Services/Implementations/EmployeeService.cs"
              ],
              "files_to_modify": [
                "HRMS.Models/Entities/Employee.cs",
                "HRMS.UI/Program.cs",
                "HRMS.Data/Configurations/EmployeeConfiguration.cs"
              ],
              "depends_on": ["T-001"],
              "acceptance_criteria": [
                "Employee entity includes Department relationship",
                "IEmployeeService implemented and registered",
                "EmployeeConfiguration updated"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["Employee.cs", "Department.cs", "ApplicationDbContext"],
                "imported_by": ["Program.cs", "EmployeesController.cs"],
                "api_routes": [],
                "db_tables": ["Employees", "Departments"],
                "env_vars": []
              }
            },
            {
              "id": "T-003",
              "name": "Implement Employee AJAX Controller Endpoints",
              "description": "Update EmployeesController with JSON endpoints utilizing IEmployeeService.",
              "files_to_create": [],
              "files_to_modify": ["HRMS.UI/Controllers/EmployeesController.cs"],
              "depends_on": ["T-002"],
              "acceptance_criteria": [
                "EmployeesController uses IEmployeeService",
                "GET /Employees/GetAll returns JSON",
                "All CRUD actions return JsonResult"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["IEmployeeService"],
                "imported_by": [],
                "api_routes": ["GET /Employees/GetAll", "POST /Employees/Create", "POST /Employees/Edit", "POST /Employees/Delete/{id}"],
                "db_tables": ["Employees"],
                "env_vars": []
              }
            },
            {
              "id": "T-004",
              "name": "Implement Employee UI with KnockoutJS and DataTables",
              "description": "Create Employee management UI with KnockoutJS ViewModel and DataTables integration.",
              "files_to_create": [
                "HRMS.UI/Views/Employees/Index.cshtml",
                "HRMS.UI/wwwroot/js/employees.js"
              ],
              "files_to_modify": [],
              "depends_on": ["T-003"],
              "acceptance_criteria": [
                "Employee Index view uses DataTables",
                "employees.js handles CRUD logic via KnockoutJS",
                "Department dropdown populated via AJAX"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["jQuery", "KnockoutJS", "DataTables"],
                "imported_by": [],
                "api_routes": ["GET /Employees/GetAll", "GET /Departments/GetAll"],
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
