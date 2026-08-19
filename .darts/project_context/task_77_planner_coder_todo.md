# Planner-Coder Todo — 77
**Requirement:** Setup HRMS.Web for background processing using Quartz.NET. Include schedulers and hosted services for scheduled tasks like report generation or email notifications.

Acceptance Criteria:
- Quartz.NET configured in HRMS.Web.
- Sample job created for attendance or notification processing.

Dependencies: Task solution_initialization

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: Microsoft.EntityFrameworkCore, HRMS.Data, ApplicationDbContext, DataFactoryServiceProvider

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: add Quartz registration and hosted service configuration
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\HRMS.Web.csproj: add Quartz and Quartz.Extensions.Hosting NuGet packages

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Configure Quartz and Implement Sample Job | HRMS.Web/HRMS.Web.csproj, HRMS.Web/Jobs/AttendanceProcessingJob.cs | pending | — |
| T-002 | Register Quartz in Program.cs | HRMS.Web/Program.cs | pending | T-001 |
