# Planner-Coder Todo — 94
**Requirement:** In the DatabaseInitializer, i don't want to delete the database each and every time the application starts. I only said that if there is no database created, then create the database similarly, if no other required tables was created then create the tables needed. 
Please check the file and make changes 
reference:
// Drop and recreate to bypass broken migration history during provider transition
_logger.LogInformation("Deleting existing database if any...");
await _context.Database.EnsureDeletedAsync();

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Implementations\DatabaseInitializer.cs: using Microsoft.EntityFrameworkCore, HRMS.Data.Interfaces, etc.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Implementations\DatabaseInitializer.cs: update InitializeAsync to check if database exists and ensure created instead of deleting every time.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Update DatabaseInitializer to avoid EnsureDeletedAsync and use EnsureCreatedAsync/MigrateAsync selectively | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Implementations\DatabaseInitializer.cs | pending | — |
