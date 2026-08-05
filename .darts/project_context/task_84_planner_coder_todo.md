# Planner-Coder Todo — 84
**Requirement:** For the given Database ConnectionString in the appsettings.json file, on application startup itself try to connect to the database, and if there is no database and tables exists for the given connectionstring then create it first and apply the initial migration and create _MigrationHistory table that will contains the list of migrations applied to it.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: AddDbContext, Migration logic (partial), Authentication, Authorization, MapControllerRoute
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: AddDbContext, Quartz configuration

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: add Database Migration logic scope similar to HRMS.UI\Program.cs

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Update Entry Points with Migration Logic | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs | pending | — |
