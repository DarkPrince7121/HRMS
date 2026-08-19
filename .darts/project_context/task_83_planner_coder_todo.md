# Planner-Coder Todo — 83
**Requirement:** Create Initial Migration & Create Database in the "(localdb)\MSSQLLocalDB". So that the application will work

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/appsettings.json: DefaultConnection already points to (localdb)\mssqllocaldb
- HRMS.Web/appsettings.json: DefaultConnection already points to (localdb)\mssqllocaldb
- HRMS.UI/Program.cs: DbContext registered with SQL Server
- HRMS.Web/Program.cs: DbContext registered with SQL Server

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Program.cs: add automatic migration application on startup

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Database Configuration and Initialization | HRMS.UI/Program.cs, HRMS.UI/appsettings.json | pending | — |
| T-002 | Infrastructure Readiness | HRMS.Web/appsettings.json | pending | T-001 |
