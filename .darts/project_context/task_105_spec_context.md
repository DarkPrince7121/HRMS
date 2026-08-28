# Spec Context — Task 105
**Generated:** 2026-08-21  |  **Framework:** KnockoutJS Backend ASP.NET  |  **Tasks:** 4

## Gap Analysis Summary
The project is a Brownfield HR Management System built with ASP.NET Core MVC and KnockoutJS. The objective is to revamp the "old/legacy" UI using modern Bootstrap 5 styling without breaking existing workflows or KnockoutJS data bindings. Key areas for enhancement include the shared layout (navigation, notifications, and new footer), the dashboard metrics, and consistent modernization of management views (Employees, Departments, etc.) and their associated forms and tables.

## Task Plan

### Module: Shared Layout

#### Feature: Global UI Shell Revamp
**T-001: Revamp Global Layout - Navigation, Notifications, and Footer**
- **Description:** Modernize the `_Layout.cshtml` file by updating the navbar to a more refined sticky-top design, enhancing the notification dropdown with better spacing and shadows, and adding a new footer section. This includes introducing a global CSS block for subtle refinements like soft shadows and improved typography.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Shared/_Layout.cshtml
- **Depends on:** None
- **Acceptance criteria:**
  - Navbar is updated to a modern dark/light variant with improved spacing.
  - Notification dropdown has a modern shadow (`shadow-sm` or `shadow`) and refined list items.
  - A responsive footer is visible at the bottom of the page.
  - All existing links and KnockoutJS bindings in the notification area function correctly.
- **Wiring:**
  - Imports from: None
  - Imported by: All views using the shared layout.
  - API routes: None
  - DB tables: None
  - Env vars: None

### Module: Dashboard

#### Feature: Dashboard Metrics Revamp
**T-002: Modernize Dashboard Metrics and Quick Links**
- **Description:** Restructure the metrics section in `Home/Index.cshtml` to use modern Bootstrap card styles (borderless with soft shadows). Improve the "Quick Links" section with more visual interest and cleaner typography while preserving the `DashboardViewModel` bindings.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Home/Index.cshtml
- **Depends on:** T-001
- **Acceptance criteria:**
  - Metric cards for Employees, Departments, and Leaves are modern and responsive.
  - Loading spinner is centered and styled consistently.
  - Quick link cards use consistent padding and button styling.
  - Dashboard counts (`employeeCount`, etc.) still update correctly via KnockoutJS.
- **Wiring:**
  - Imports from: ~/js/dashboard.js
  - Imported by: None
  - API routes: GET /api/Dashboard/Metrics (assumed via dashboard.js)
  - DB tables: Employees, Departments, LeaveRequests
  - Env vars: None

### Module: Employee Management

#### Feature: Employee View and Form Modernization
**T-003: Revamp Employee List and Management Form**
- **Description:** Update `Employees/Index.cshtml` and the shared `_EmployeeFormPartial.cshtml` to use modern Bootstrap components. Focus on refining the DataTables container, action buttons, and modal form layout while ensuring every `data-bind` attribute remains untouched for logic preservation.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Employees/Index.cshtml, HRMS.UI/Views/Shared/_EmployeeFormPartial.cshtml
- **Depends on:** T-001
- **Acceptance criteria:**
  - Employee table uses modern row styling and clear action buttons.
  - The "Add Employee" modal form is visually cleaned up with better spacing and field grouping.
  - KnockoutJS bindings for form submission and table population are fully functional.
- **Wiring:**
  - Imports from: ~/js/employees.js (assumed)
  - Imported by: None
  - API routes: GET /api/Employees, POST /api/Employees
  - DB tables: Employees
  - Env vars: None

### Module: Administration & Support Modules

#### Feature: Bulk View Modernization
**T-004: Standardize UI for Departments, Attendance, Leaves, and Settings**
- **Description:** Systematically update all remaining management views to match the new design language. This involves applying modern card containers, refined buttons, and standardized spacing across all administrative modules.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Departments/Index.cshtml, HRMS.UI/Views/Attendance/Index.cshtml, HRMS.UI/Views/Leave/Index.cshtml, HRMS.UI/Views/Payroll/Index.cshtml, HRMS.UI/Views/Designations/Index.cshtml, HRMS.UI/Views/Holidays/Index.cshtml, HRMS.UI/Views/Audit/Index.cshtml, HRMS.UI/Views/Settings/Index.cshtml
- **Depends on:** T-001
- **Acceptance criteria:**
  - Consistent visual theme applied across all listed views.
  - DataTables in these views are styled uniformly.
  - All existing workflows (CRUD operations) remain functional.
- **Wiring:**
  - Imports from: Respective JS files in ~/wwwroot/js/
  - Imported by: None
  - API routes: Various management endpoints
  - DB tables: Departments, Attendance, LeaveRequests, Payroll, Designations, Holidays, AuditLogs, Settings
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Shared Layout",
      "features": [
        {
          "feature": "Global UI Shell Revamp",
          "tasks": [
            {
              "id": "T-001",
              "name": "Revamp Global Layout - Navigation, Notifications, and Footer",
              "description": "Modernize the _Layout.cshtml file by updating the navbar, enhancing the notification dropdown, and adding a footer.",
              "files_to_create": [],
              "files_to_modify": ["HRMS.UI/Views/Shared/_Layout.cshtml"],
              "depends_on": [],
              "acceptance_criteria": [
                "Navbar updated to modern design",
                "Notification dropdown uses shadows and refined list items",
                "Responsive footer added",
                "KnockoutJS bindings in layout remain functional"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [],
                "imported_by": ["All Views"],
                "api_routes": [],
                "db_tables": [],
                "env_vars": []
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Dashboard",
      "features": [
        {
          "feature": "Dashboard Metrics Revamp",
          "tasks": [
            {
              "id": "T-002",
              "name": "Modernize Dashboard Metrics and Quick Links",
              "description": "Restructure metrics cards and quick links in Home/Index.cshtml with modern styling while preserving bindings.",
              "files_to_create": [],
              "files_to_modify": ["HRMS.UI/Views/Home/Index.cshtml"],
              "depends_on": ["T-001"],
              "acceptance_criteria": [
                "Metric cards are borderless with soft shadows",
                "Quick link section is modernized",
                "KnockoutJS data-binds are functional"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["~/js/dashboard.js"],
                "imported_by": [],
                "api_routes": ["GET /api/Dashboard/Metrics"],
                "db_tables": ["Employees", "Departments", "LeaveRequests"],
                "env_vars": []
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Employee Management",
      "features": [
        {
          "feature": "Employee View and Form Modernization",
          "tasks": [
            {
              "id": "T-003",
              "name": "Revamp Employee List and Management Form",
              "description": "Update Employees index and the shared employee form partial with modern Bootstrap components.",
              "files_to_create": [],
              "files_to_modify": ["HRMS.UI/Views/Employees/Index.cshtml", "HRMS.UI/Views/Shared/_EmployeeFormPartial.cshtml"],
              "depends_on": ["T-001"],
              "acceptance_criteria": [
                "Employee table uses modern styling",
                "Modal form layout is cleaned up",
                "Data-bind logic is preserved"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["~/js/employees.js"],
                "imported_by": [],
                "api_routes": ["GET /api/Employees", "POST /api/Employees"],
                "db_tables": ["Employees"],
                "env_vars": []
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Administration & Support Modules",
      "features": [
        {
          "feature": "Bulk View Modernization",
          "tasks": [
            {
              "id": "T-004",
              "name": "Standardize UI for Departments, Attendance, Leaves, and Settings",
              "description": "Systematically update remaining management views to match the new design language.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Views/Departments/Index.cshtml",
                "HRMS.UI/Views/Attendance/Index.cshtml",
                "HRMS.UI/Views/Leave/Index.cshtml",
                "HRMS.UI/Views/Payroll/Index.cshtml",
                "HRMS.UI/Views/Designations/Index.cshtml",
                "HRMS.UI/Views/Holidays/Index.cshtml",
                "HRMS.UI/Views/Audit/Index.cshtml",
                "HRMS.UI/Views/Settings/Index.cshtml"
              ],
              "depends_on": ["T-001"],
              "acceptance_criteria": [
                "Consistent theme across all administrative views",
                "DataTables styled uniformly",
                "Workflows remain functional"
              ],
              "status": "pending",
              "wiring": {
                "imports_from": ["Various module-specific JS files"],
                "imported_by": [],
                "api_routes": ["Various management endpoints"],
                "db_tables": ["Multiple"],
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
