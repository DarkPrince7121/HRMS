# Planner-Coder Todo — 104
**Requirement:** After trying to login with an account, i'm getting this error: 42703: column e.UserId does not exist POSITION: 90. The userid column was not added in the database table of employees, since there is no migrations file created. create a new migration file for adding userid column in employees table and apply the migration on application startup.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.UI\Program.cs: AppContext.SetSwitch, AddControllersWithViews, AddDbContext, AddScoped registrations, AddAuthentication, AddCookie, AddAuthorization, app.Services.CreateScope with context.Database.Migrate() and initializer.InitializeAsync(), app.UseHttpsRedirection, app.UseStaticFiles, app.UseRouting, app.UseAuthentication, app.UseAuthorization, app.MapControllerRoute.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.Web\Program.cs: AddDbContext, AddScoped<IDataFactory>, AddQuartz, AddQuartzHostedService, app.Services.CreateScope with context.Database.Migrate().

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.Data\Migrations\<Timestamp>_AddUserIdToEmployee.cs: New EF Core migration file to add UserId column to Employees table.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.Data\Migrations\ApplicationDbContextModelSnapshot.cs: Update model snapshot to include UserId on Employee.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Create EF Core Migration for UserId | HRMS.Data/Migrations/20231027000000_AddUserIdToEmployee.cs, HRMS.Data/Migrations/20231027000000_AddUserIdToEmployee.Designer.cs, HRMS.Data/Migrations/ApplicationDbContextModelSnapshot.cs | pending | — |
| T-002 | Entry points — Verify Migration Application | HRMS.UI/Program.cs, HRMS.Web/Program.cs | pending | T-001 |
