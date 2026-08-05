# Planner-Coder Todo — 78
**Requirement:** Implement auxiliary modules: Designation, Holiday Calendar, and Audit Logging to track system changes.

Acceptance Criteria:
- Designation and Holiday Calendar views functional.
- Audit log table capturing entity changes.

Dependencies: Task core_employee_dept_modules_crud

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: DbSet registrations for Employee, Department, User, Role, Attendance, Leave, Payroll.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: Service registrations for IDepartmentService, IEmployeeService, etc.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Shared\_Layout.cshtml: Existing nav links.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: add DbSet<Designation>, DbSet<Holiday>, DbSet<AuditLog> and call ApplyConfiguration in OnModelCreating.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: add builder.Services.AddScoped<IDesignationService, DesignationService>(), builder.Services.AddScoped<IHolidayService, HolidayService>(), builder.Services.AddScoped<IAuditService, AuditService>().
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Shared\_Layout.cshtml: add nav links for Designations, Holidays, and Audit Logs.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — ALL models + ALL controllers + DbContext updates | HRMS.Models/Entities/{Designation.cs, Holiday.cs, AuditLog.cs}, HRMS.Models/DTOs/{DesignationRequest.cs, DesignationResponse.cs, HolidayRequest.cs, HolidayResponse.cs, AuditLogResponse.cs}, HRMS.Data/Configurations/{DesignationConfiguration.cs, HolidayConfiguration.cs, AuditLogConfiguration.cs}, HRMS.Data/ApplicationDbContext.cs, HRMS.UI/Controllers/{DesignationsController.cs, HolidaysController.cs, AuditController.cs} | pending | — |
| T-002 | Entry points — Program.cs / Startup, Layout updates | HRMS.UI/Program.cs, HRMS.UI/Views/Shared/_Layout.cshtml | pending | T-001 |
| T-003 | Frontend services — ALL API service files | HRMS.Services/Interfaces/{IDesignationService.cs, IHolidayService.cs, IAuditService.cs}, HRMS.Services/{DesignationService.cs, HolidayService.cs, AuditService.cs} | pending | T-001 |
| T-004 | Frontend UI — ALL components + CSS + App.tsx/router updates | HRMS.UI/Views/Designations/Index.cshtml, HRMS.UI/Views/Holidays/Index.cshtml, HRMS.UI/Views/Audit/Index.cshtml, HRMS.UI/wwwroot/js/{designations.js, holidays.js, audit.js} | pending | T-003 |
