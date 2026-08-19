# Planner-Coder Todo — 72
**Requirement:** Configure EF Core in HRMS.Data using SQL Server LocalDB. Implement IDataFactory and DataFactoryServiceProvider. Setup initial DbContext with required Fluent API mappings for core entities.

Acceptance Criteria:
- DbContext is configured with SQL Server LocalDB.
- IDataFactory and DataFactory implementations exist.
- Initial migration is created for base entities.

Dependencies: Task solution_initialization

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: builder.Services.AddDbContext<ApplicationDbContext>, builder.Services.AddScoped<IDataFactory, DataFactoryServiceProvider>

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: update options.UseNpgsql to options.UseSqlServer
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\appsettings.json: add ConnectionStrings for SQL Server LocalDB

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend Configuration — Update DbContext to SQL Server & Add Connection String | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\appsettings.json | pending | — |
| T-002 | Migration — Create Initial Migration | InitialMigration.cs | pending | T-001 |
