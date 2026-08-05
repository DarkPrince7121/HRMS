# Planner-Coder Todo — 80
**Requirement:** Create the Dashboard module providing high-level metrics and visual summaries using AJAX/Knockout bindings.

Acceptance Criteria:
- Dashboard displays counts for Employees, Departments, and active Leaves.
- Knockout observables update charts/widgets.

Dependencies: Task core_employee_dept_modules_crud

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\HomeController.cs: [Authorize], returns View()
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Home\Index.cshtml: Basic navigation cards
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: Services for IDepartmentService, IEmployeeService, ILeaveService registered

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\HomeController.cs: Add API endpoint `GetDashboardStats`
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Home\Index.cshtml: Add Knockout bindings, metric cards, and dashboard script

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend Dashboard API | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\HomeController.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Models\DTOs\DashboardStatsResponse.cs | pending | — |
| T-002 | Frontend Dashboard UI | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Home\Index.cshtml, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\dashboard.js | pending | T-001 |
