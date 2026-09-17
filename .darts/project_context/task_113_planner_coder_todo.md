# Planner-Coder Todo — 113
**Requirement:** Create database entity model and EF Core migration for EmployeeSalaryBalance table. This table tracks the payroll balance processed for each employee. It must contain the columns: EmployeeId, AmountBalance.

Acceptance Criteria:
- Create EF Core migration for EmployeeSalaryBalance table
- Verify fields: EmployeeId (or equivalent unique identifier matching system's employee model), AmountBalance (decimal)
- Apply migrations to the PostgreSQL database successfully

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\ApplicationDbContext.cs: ApplicationDbContext contains DbSet<EmployeeSalaryBalance> and maps EmployeeSalaryBalanceConfiguration.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Models\Entities\EmployeeSalaryBalance.cs: EmployeeSalaryBalance entity model containing EmployeeId and AmountBalance.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\Configurations\EmployeeSalaryBalanceConfiguration.cs: Entity mapping configuration for EmployeeSalaryBalance.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\Migrations\20260902073901_AddEmployeeSalaryBalanceTable.cs: EF Core migration for EmployeeSalaryBalance table.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- None (already implemented)

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Verify and apply existing database entity model and EF Core migration for EmployeeSalaryBalance table | HRMS.Models/Entities/EmployeeSalaryBalance.cs, HRMS.Data/Configurations/EmployeeSalaryBalanceConfiguration.cs, HRMS.Data/ApplicationDbContext.cs, HRMS.Data/Migrations/20260902073901_AddEmployeeSalaryBalanceTable.cs | pending | — |
