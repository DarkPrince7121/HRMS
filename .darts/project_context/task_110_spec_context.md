# Spec Context — Task 110
**Generated:** 2026-08-28  |  **Framework:** KnockoutJS Backend ASP.NET  |  **Tasks:** 1

## Gap Analysis Summary
The gap analysis identified that employees with existing approved leaves (such as Casual Leave, Annual Leave, etc.) receive zero deductions during payroll generation. This is because the service layer looks for a leave type containing "Unpaid", which does not exist in the system's leave type options. The goal is to modify the existing backend logic in `HRMS.Services/PayrollService.cs` to treat all approved leave types as deductions, unless they are "sick", "maternity", or "paternity" leaves.

## Task Plan

### Module: Payroll Management

#### Feature: Leave Deduction Rules Realignment

**T-001: Realign leave deduction rules in PayrollService**
- **Description:** Update `GetPayrollBreakdownAsync` in `HRMS.Services/PayrollService.cs` so that approved leave records are treated as deductions unless the leave type contains "sick", "maternity", or "paternity" (case-insensitive). Removes the outdated check for "Unpaid" leave type.
- **Files to create:** None
- **Files to modify:** HRMS.Services/PayrollService.cs
- **Depends on:** None
- **Acceptance criteria:**
  - Approved leave types such as "Casual Leave", "Annual Leave", or any other type not matching "sick", "maternity", or "paternity" must be included in the deduction calculation during payroll processing.
  - Approved leave types matching "sick", "maternity", or "paternity" must result in zero deduction.
  - Calculations must correctly apply daily rate (baseSalary / 22) times overlap days.
- **Wiring:**
  - Imports from: HRMS.Models.Entities.Leave, HRMS.Data.ApplicationDbContext
  - Imported by: HRMS.Services.PayrollService
  - API routes: None
  - DB tables: Leaves, Payrolls, Employees
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Payroll Management",
      "features": [
        {
          "feature": "Leave Deduction Rules Realignment",
          "tasks": [
            {
              "id": "T-001",
              "name": "Realign leave deduction rules in PayrollService",
              "description": "Update GetPayrollBreakdownAsync in HRMS.Services/PayrollService.cs so that approved leave records are treated as deductions unless the leave type contains 'sick', 'maternity', or 'paternity' (case-insensitive). Removes the outdated check for 'Unpaid' leave type.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.Services/PayrollService.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Approved leave types such as 'Casual Leave', 'Annual Leave', or any other type not matching 'sick', 'maternity', or 'paternity' must be included in the deduction calculation during payroll processing.",
                "Approved leave types matching 'sick', 'maternity', or 'paternity' must result in zero deduction.",
                "Calculations must correctly apply daily rate (baseSalary / 22) times overlap days."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models.Entities.Leave",
                  "HRMS.Data.ApplicationDbContext"
                ],
                "imported_by": [
                  "HRMS.Services.PayrollService"
                ],
                "api_routes": [],
                "db_tables": [
                  "Leaves",
                  "Payrolls",
                  "Employees"
                ],
                "env_vars": []
              }
            }
          ]
        }
      ]
    }
  ]
}
```