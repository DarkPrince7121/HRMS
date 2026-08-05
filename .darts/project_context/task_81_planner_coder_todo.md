# Planner-Coder Todo — 81
**Requirement:** Implement the Notifications and Settings module. Includes system-wide settings and user-specific notification alerts.

Acceptance Criteria:
- Notification entity and Service implemented.
- UI notification bell or toast updates on new events.

Dependencies: Task background_jobs_infrastructure

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: Employees, Departments, Users, Roles, Attendances, Leaves, Payrolls, Designations, Holidays, AuditLogs
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: IDepartmentService, IEmployeeService, IAttendanceService, ILeaveService, IPayrollService, IDesignationService, IHolidayService, IAuditService
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Shared\_Layout.cshtml: Navbar links for Departments, Employees, etc.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: add DbSet<Notification> Notifications, DbSet<SystemSetting> SystemSettings
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: add INotificationService, ISettingService
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Shared\_Layout.cshtml: add notification bell icon and Settings link

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — ALL models + ALL controllers + DbContext updates | HRMS.Models/Entities/Notification.cs, HRMS.Models/Entities/SystemSetting.cs, HRMS.Models/DTOs/NotificationResponse.cs, HRMS.Models/DTOs/SettingRequest.cs, HRMS.Data/ApplicationDbContext.cs, HRMS.Data/Configurations/NotificationConfiguration.cs, HRMS.Data/Configurations/SystemSettingConfiguration.cs, HRMS.UI/Controllers/NotificationsController.cs, HRMS.UI/Controllers/SettingsController.cs | pending | — |
| T-002 | Entry points — Program.cs registration | HRMS.UI/Program.cs | pending | T-001 |
| T-003 | Frontend services — ALL API service files | HRMS.Services/Interfaces/INotificationService.cs, HRMS.Services/Interfaces/ISettingService.cs, HRMS.Services/NotificationService.cs, HRMS.Services/SettingService.cs | pending | T-001 |
| T-004 | Frontend UI — ALL components + CSS + App.tsx/router updates | HRMS.UI/Views/Shared/_Layout.cshtml, HRMS.UI/wwwroot/js/notifications.js, HRMS.UI/wwwroot/js/settings.js, HRMS.UI/Views/Settings/Index.cshtml | pending | T-003 |
