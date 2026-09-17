# Gap Context — As an user, if i generate Payroll then the salary should get added to the employeesalarybalances against the each employees.
also, lets say if the user creates an application with an amount entered then the amount should get reduced from the employeesalarybalances table against the same employee.. there should be an relationship between the employeeid column in employeesalarybalance and the id of employees table. go through the requirement that i have given, because of i try to create a new application, there the amount displays as zero since there is no record in the employeesalarybalances table even after generating multiple payroll, this issue repeats. analyze the root cause and fix this
**Date:** 2026-09-03  |  **Task ID:** 119  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- ASP.NET Core 8.0 (Target Framework)
- Entity Framework Core (PostgreSQL Provider)
- KnockoutJS (Frontend MVVM)
- PostgreSQL (Database)

### Existing Modules & Features
- **Payroll Service** (`HRMS.Services/PayrollService.cs`): Handles monthly payroll generation, deductions (leaves/absences), and net pay calculation.
- **Applications Controller** (`HRMS.UI/Controllers/ApplicationsController.cs`): Manages withdrawal applications, status updates, and balance deductions upon completion.
- **Data Models** (`HRMS.Models/Entities/`): Contains `EmployeeSalaryBalance`, `Payroll`, and `WithdrawalApplication` entities.
- **Database Context** (`HRMS.Data/ApplicationDbContext.cs`): EF Core context with entity configurations.

### Prior Context
No prior analysis found for this specific issue.

## Requirements Analysis

### Extracted Requirements
1. **Update Salary Balance on Payroll Generation**: When a payroll record is generated/processed, the `NetPay` amount must be added to the `AmountBalance` in the `EmployeeSalaryBalances` table for that employee.
2. **Deduct Salary Balance on Application Completion**: When a withdrawal application is completed/submitted, the amount must be reduced from the `EmployeeSalaryBalances`.
3. **Establish/Verify Relationship**: Ensure a foreign key relationship exists between `EmployeeSalaryBalance.EmployeeId` and `Employee.Id`.
4. **Fix Root Cause of Zero Balance**: Investigate why balances remain zero even after multiple payroll generations. The analysis reveals that `PayrollService.GeneratePayrollForMonthAsync` saves payroll records but never updates or initializes the `EmployeeSalaryBalances` table.
5. **Handle Missing Balance Records**: If a payroll is generated for an employee who doesn't have a record in `EmployeeSalaryBalances`, one must be created (upsert logic).

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Update Salary Balance on Payroll Generation | Needs Modification | `HRMS.Services/PayrollService.cs` | `GeneratePayrollForMonthAsync` lacks balance update logic. |
| Deduct Salary Balance on Application Completion | Already Exists | `HRMS.UI/Controllers/ApplicationsController.cs` | `CompleteApplication` method already implements deduction logic. |
| Establish Relationship | Already Exists | `HRMS.Data/Configurations/EmployeeSalaryBalanceConfiguration.cs` | Relationship is already configured as a one-to-one mapping. |
| Fix Root Cause of Zero Balance | Needs Modification | `HRMS.Services/PayrollService.cs` | The service logic completely ignores the balance table during generation. |

## Tech Stack & Implementation

### Payroll Balance Integration — Needs Modification
- **Approach:** Modify the payroll generation logic to iterate through generated payroll records and update the corresponding `EmployeeSalaryBalance`. Implementation should use an "upsert" pattern: check if a balance record exists for the `EmployeeId`; if yes, increment `AmountBalance` by the `NetPay`; if no, create a new record with `AmountBalance` equal to `NetPay`. This ensures the balance reflects accumulated earnings.
- **Existing files to modify:** `HRMS.Services/PayrollService.cs`
- **New dependencies:** None

### Application Balance Verification — Needs Modification (Implicit)
- **Approach:** While the deduction logic exists in the API, the requirement notes that "the amount displays as zero" when creating a new application. This suggests that the UI (KnockoutJS) or the API endpoint for initiating/getting applications might need to be verified to ensure it's fetching and displaying the current balance to the user, though the primary fix is ensuring the data exists in the database first.
- **Existing files to modify:** `HRMS.UI/Controllers/ApplicationsController.cs`, `HRMS.UI/wwwroot/js/applications.js`
- **New dependencies:** None

## Summary
The project is a brownfield ASP.NET Core application with a PostgreSQL backend and KnockoutJS frontend. It has a functional Payroll and Withdrawal Application system, but they are currently disconnected in terms of financial data flow. Specifically, while the database schema supports employee salary balances, the business logic for generating payroll does not populate or update these balances.

The primary task is to bridge this gap by modifying `PayrollService` to ensure that every time a monthly payroll is processed, the resulting net pay is added to the employee's accumulated balance. This will resolve the root cause where users see a zero balance despite payrolls being generated. The existing withdrawal logic in `ApplicationsController` is already designed to deduct from these balances, so once the payroll generation is fixed, the end-to-end flow (Earn -> Balance -> Withdraw) will be functional. Overall, the implementation is a modification of existing service-layer logic to ensure data consistency across the payroll and application modules.