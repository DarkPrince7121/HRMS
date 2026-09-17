# Spec Context — Task 119
**Generated:** 2026-09-03  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 1

## Gap Analysis Summary
The HRMS application generates payroll records but fails to update the `EmployeeSalaryBalances` table, resulting in zero balances for employees. The `PayrollService` correctly calculates net pay but lacks the logic to "upsert" these amounts into the balance table. Additionally, although withdrawal applications deduct from the balance upon completion, the lack of initial funding from payroll prevents the end-to-end flow from working. This task will integrate balance updates into the payroll generation process to ensure data consistency.

## Task Plan

### Module: Payroll & Finance

#### Feature: Salary Balance Management

**T-001: Implement Payroll Balance Synchronization and Upsert Logic**
- **Description:** Modify `PayrollService.GeneratePayrollForMonthAsync` to update the `EmployeeSalaryBalances` table whenever payroll is processed. For each employee, the system must check if a balance record exists; if it does, add the `NetPay` to `AmountBalance`; if not, create a new record. This ensures that payroll generation directly funds the employee's withdrawal capacity.
- **Files to create:** None
- **Files to modify:** HRMS.Services/PayrollService.cs
- **Depends on:** None
- **Acceptance criteria:**
  - After running "Generate Payroll", the `EmployeeSalaryBalances` table contains records for all processed employees.
  - Generating payroll multiple times correctly increments the `AmountBalance`.
  - The `EmployeeId` in `EmployeeSalaryBalances` correctly references the `Employees` table.
  - The Withdrawal Application UI correctly displays the updated balance when an employee is selected.
- **Wiring:**
  - Imports from: HRMS.Data/ApplicationDbContext.cs, HRMS.Models/Entities/EmployeeSalaryBalance.cs
  - Imported by: HRMS.UI/Controllers/PayrollController.cs (via IPayrollService)
  - API routes: POST /api/payroll/generate (indirectly via Service call)
  - DB tables: Payrolls, EmployeeSalaryBalances, Employees
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Payroll & Finance",
      "features": [
        {
          "feature": "Salary Balance Management",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Payroll Balance Synchronization and Upsert Logic",
              "description": "Modify PayrollService.GeneratePayrollForMonthAsync to update the EmployeeSalaryBalances table whenever payroll is processed. For each employee, the system must check if a balance record exists; if it does, add the NetPay to AmountBalance; if not, create a new record. This ensures that payroll generation directly funds the employee's withdrawal capacity.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.Services/PayrollService.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "After running 'Generate Payroll', the EmployeeSalaryBalances table contains records for all processed employees.",
                "Generating payroll multiple times correctly increments the AmountBalance.",
                "The EmployeeId in EmployeeSalaryBalances correctly references the Employees table.",
                "The Withdrawal Application UI correctly displays the updated balance when an employee is selected."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Data/ApplicationDbContext.cs",
                  "HRMS.Models/Entities/EmployeeSalaryBalance.cs"
                ],
                "imported_by": [
                  "HRMS.UI/Controllers/PayrollController.cs"
                ],
                "api_routes": [
                  "POST /api/payroll/generate"
                ],
                "db_tables": [
                  "Payrolls",
                  "EmployeeSalaryBalances",
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
