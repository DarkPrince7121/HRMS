# Planner-Coder Todo — 88
**Requirement:** Still there is one table created... check the logic and find the root cause and fix it. below is the console logs.

The logs indicate two main issues:
1. `Microsoft.Data.SqlClient.SqlException (0x80131904): Invalid object name 'Roles'.` - This happens during seeding, even though migrations are supposedly up to date. This suggests that the `Roles` table might not exist in the database, possibly due to a mismatch between migrations and the current schema or a failed previous migration state.
2. `warn: Microsoft.EntityFrameworkCore.Model.Validation[30000] No store type was specified for the decimal property 'BaseSalary' on entity type 'Employee'.` - This is a missing configuration for the decimal property.

The "one table created" symptom usually means EF created the `__EFMigrationsHistory` table but failed to create the actual domain tables (like `Roles`) or the user is looking at a database where only one table is visible despite migrations reporting success. However, `MigrateAsync()` reporting "No migrations were applied" while `Roles` table is missing suggests the `__EFMigrationsHistory` table thinks the initial migration is applied, but the actual tables are not there.

Plan:
1. Fix the `Employee` decimal precision warning in `EmployeeConfiguration.cs`.
2. Ensure `RoleConfiguration` and other configurations are correctly mapping to tables.
3. Since the database is in a corrupted state (history says migrated, but tables missing), I will modify `DatabaseInitializer` to ensure the database is deleted and recreated if it's inconsistent or ensure the schema is explicitly checked. However, a safer approach for this specific fix is to ensure the configurations are perfect and let the developer reset the DB if needed, but I'll add the missing decimal precision fix.
4. Actually, the error `Invalid object name 'Roles'` during `SeedRolesAsync` confirms the table is missing. If `MigrateAsync` says "No migrations were applied", it means the `__EFMigrationsHistory` table exists and contains the migration ID, but the actual `Roles` table does not exist.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\ApplicationDbContext.cs: Employees, Departments, Users, Roles, Attendances, Leaves, Payrolls, Designations, Holidays, AuditLogs, Notifications, SystemSettings DbSets and configurations registered.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Configurations\EmployeeConfiguration.cs: add `.HasPrecision(18, 2)` or `.HasColumnType("decimal(18,2)")` to `BaseSalary`.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Configurations\RoleConfiguration.cs: explicitly call `builder.ToTable("Roles")` to be safe, although EF usually does this by convention.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Fix decimal precision and explicit table mapping | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Configurations\EmployeeConfiguration.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Configurations\RoleConfiguration.cs | pending | — |
| T-002 | Database Initializer — Force schema check | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Data\Implementations\DatabaseInitializer.cs | pending | T-001 |
