# Planner-Coder Todo — 73
**Requirement:** Implement role-based authentication and authorization. Includes User and Role entities, Authentication controller, and login views using the Knockout.js/AJAX pattern.

Acceptance Criteria:
- Identity or custom role-based middleware is active.
- Login page authenticates against database.
- Controllers use Authorize attributes.

Dependencies: Task database_persistence_setup

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: AddControllersWithViews, UseAuthentication, UseAuthorization, CookieAuthentication registered.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: Users, Roles, Employees DbSet registered.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\EmployeesController.cs: Add [Authorize] attribute.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Models, Controllers, DbContext | HRMS.UI/Controllers/EmployeesController.cs | pending | — |
| T-002 | Entry points — Program.cs registration | HRMS.UI/Program.cs | pending | T-001 |
| T-003 | Frontend services — API service files | HRMS.UI/wwwroot/js/auth.js | pending | T-001 |
| T-004 | Frontend UI — Views and UI components | HRMS.UI/Views/Employees/Index.cshtml | pending | T-003 |
