# Planner-Coder Todo — 87
**Requirement:** On application startup, the console shows a "password authentication failed for user 'postgres'" error when using Npgsql. The user also asked why `UseNpgsql` is used instead of `UseSqlServer` in `Program.cs`. 

The goal is to fix the authentication error and switch the database provider from PostgreSQL to SQL Server as requested.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Program.cs: uses Npgsql, ApplicationDbContext with UseNpgsql
- HRMS.Web/Program.cs: uses Npgsql, ApplicationDbContext with UseNpgsql
- HRMS.Data/ApplicationDbContext.cs: standard DbContext
- HRMS.UI/appsettings.json: PostgreSQL connection string
- HRMS.Web/appsettings.json: SQL Server connection string (incorrectly present while code uses Npgsql)

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Program.cs: replace `options.UseNpgsql` with `options.UseSqlServer`
- HRMS.Web/Program.cs: replace `options.UseNpgsql` with `options.UseSqlServer`
- HRMS.UI/appsettings.json: update connection string to SQL Server format
- HRMS.Web/appsettings.json: verify/fix SQL Server connection string format

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Update Database Provider to SQL Server in Backend | HRMS.UI/Program.cs, HRMS.Web/Program.cs, HRMS.UI/appsettings.json, HRMS.Web/appsettings.json | pending | — |
| T-002 | Cleanup unused PostgreSQL references (Optional but recommended for consistency) | HRMS.UI/HRMS.UI.csproj, HRMS.Web/HRMS.Web.csproj, HRMS.Data/HRMS.Data.csproj | pending | T-001 |
