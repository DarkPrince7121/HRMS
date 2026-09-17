# Todo List — Task 115
**Generated:** 2026-09-02T08:27:25Z  |  **Total Tasks:** 2  |  **Framework:** KnockoutJS ASP.NET Core PostgreSQL

---

## Progress Summary

| Status | Count |
|---|---|
| pending | 2 |
| in_progress | 0 |
| completed | 0 |
| failed | 0 |
| **Total** | **2** |

---

## Module: Withdrawal Workflow Management

### Feature: Active Employee Metadata & Calculated Balances

| ID | Task | Status | Files |
|---|---|---|---|
| T-001 | Implement Employee API endpoints for metadata and net balance retrieval | completed | `HRMS.Models/DTOs/EmployeeApiDto.cs`, `HRMS.Models/DTOs/EmployeeBalanceDto.cs`, `HRMS.UI/Controllers/EmployeesApiController.cs`, `HRMS.UI/Program.cs` |

### Feature: Withdrawal Application Workflow

| ID | Task | Status | Files |
|---|---|---|---|
| T-002 | Implement Withdrawal Application workflow lifecycle and safe completion APIs | in_progress | `HRMS.Models/DTOs/WithdrawalApplicationDto.cs`, `HRMS.Models/DTOs/UpdateStatusDto.cs`, `HRMS.UI/Controllers/ApplicationsController.cs`, `HRMS.UI/Program.cs` |

---

## All Tasks (flat list for coding agent)

| ID | Module | Feature | Task | Status | Depends On |
|---|---|---|---|---|---|
| T-001 | Withdrawal Workflow Management | Active Employee Metadata & Calculated Balances | Implement Employee API endpoints for metadata and net balance retrieval | completed | — |
| T-002 | Withdrawal Workflow Management | Withdrawal Application Workflow | Implement Withdrawal Application workflow lifecycle and safe completion APIs | in_progress | T-001 |
