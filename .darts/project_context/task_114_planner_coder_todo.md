# Planner-Coder Todo — 114
**Requirement:** Create database entity model and migration for the WithdrawalApplication table. It must contain the following fields verbatim: Id, StatusId, CreatedPersonId, CreatedDate, LastChangeDate, StatusLastChangeDate, Amount, SelectedEmployeeId.

Acceptance Criteria:
- Create EF Core migration for WithdrawalApplication table
- Verify fields: Id, StatusId (foreign key to ApplicationStatus), CreatedPersonId (author/creator of application), CreatedDate, LastChangeDate, StatusLastChangeDate, Amount (decimal), SelectedEmployeeId (nullable/target employee)
- Apply migration to PostgreSQL database successfully

Technical Hints: Configure foreign key relations from StatusId to the ApplicationStatus table. Ensure default values or timestamps are configured for date fields in PostgreSQL.

Dependencies: Task db-migration-application-status

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.Data/ApplicationDbContext.cs: WithdrawalApplications DbSet and Configuration are registered.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- None (Already implemented)

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Create and configure WithdrawalApplication model, DbContext registration, and migration | HRMS.Models/Entities/WithdrawalApplication.cs, HRMS.Data/Configurations/WithdrawalApplicationConfiguration.cs, HRMS.Data/ApplicationDbContext.cs, HRMS.Data/Migrations/20260902074721_AddWithdrawalApplicationTable.cs | completed | — |
