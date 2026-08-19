# Planner-Coder Todo — 79
**Requirement:** Enhance all DataTables with advanced filtering, search models, and export capabilities as requested in the frontend architecture.

Acceptance Criteria:
- Export buttons functional on DataTables (CSV/Excel/PDF).
- Search filters correctly reload DataTables via AJAX.

Dependencies: Task core_employee_dept_modules_crud

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Views/Shared/_Layout.cshtml: bootstrap, datatables, knockout, jquery
- HRMS.UI/Controllers/EmployeesController.cs: GetAll action
- HRMS.UI/Controllers/DepartmentsController.cs: GetAll action
- HRMS.UI/wwwroot/js/employees.js: EmployeeViewModel, DataTable initialization
- HRMS.UI/wwwroot/js/departments.js: DepartmentViewModel, DataTable initialization

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Views/Shared/_Layout.cshtml: add DataTables Buttons CSS/JS and JSZip/pdfmake libraries
- HRMS.UI/Controllers/EmployeesController.cs: update GetAll to accept filter parameters
- HRMS.UI/Controllers/DepartmentsController.cs: update GetAll to accept filter parameters
- HRMS.UI/wwwroot/js/employees.js: add search filter observables and update DataTable options
- HRMS.UI/wwwroot/js/departments.js: add search filter observables and update DataTable options

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Update Controllers and DTOs for filtering | HRMS.UI/Controllers/EmployeesController.cs, HRMS.UI/Controllers/DepartmentsController.cs, HRMS.UI/Controllers/DesignationsController.cs, HRMS.Models/DTOs/SearchRequest.cs | pending | — |
| T-002 | Entry points — Update Layout for DataTables Buttons | HRMS.UI/Views/Shared/_Layout.cshtml | pending | T-001 |
| T-003 | Frontend UI — Enhance JS and Views with Export and Advanced Search | HRMS.UI/wwwroot/js/employees.js, HRMS.UI/wwwroot/js/departments.js, HRMS.UI/wwwroot/js/designations.js, HRMS.UI/Views/Employees/Index.cshtml, HRMS.UI/Views/Departments/Index.cshtml, HRMS.UI/Views/Designations/Index.cshtml | pending | T-002 |
