# Planner-Coder Todo — 112
**Requirement:** Create the database migration and entity model for ApplicationStatus. The table must contain: Id (integer or guid), Name (string). Populate the table with seed data for 'Application Created', 'User Selected', 'Amount Entered', and 'Application Submitted'.

Acceptance Criteria:
- Create EF Core migration for ApplicationStatus table with Name column
- Seed ApplicationStatus with: 'Application Created', 'User Selected', 'Amount Entered', 'Application Submitted'
- Run migration against PostgreSQL to ensure table is created and seeded successfully

Technical Hints: Use EF Core HasData in OnModelCreating to seed statuses. Ensure the table is mapped to PostgreSQL.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\ApplicationDbContext.cs: ApplicationDbContext registered DbSets and OnModelCreating.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Models\Entities\ApplicationStatus.cs: add ApplicationStatus entity model
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\Configurations\ApplicationStatusConfiguration.cs: add Configuration file for ApplicationStatus and HasData seeding
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\ApplicationDbContext.cs: add DbSet<ApplicationStatus> and apply ApplicationStatusConfiguration

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Create ApplicationStatus Entity and Configuration with HasData Seeding, and update ApplicationDbContext | C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Models\Entities\ApplicationStatus.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\Configurations\ApplicationStatusConfiguration.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\ApplicationDbContext.cs | pending | — |
| T-002 | Add EF Core Migration for ApplicationStatus and Apply it | C:\DARTS-development-environment\sandbox\rmanoj\HRMS-ApplicationProcess\HRMS.Data\Migrations\ | pending | T-001 |
