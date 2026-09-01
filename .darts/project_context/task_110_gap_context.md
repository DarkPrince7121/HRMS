# Gap Context — As an user, if i process payroll, for the users with existing leave also getting zero deductions. so unless or until the leave type is sick/maternity/paternity others are considered as deduction. in the code i saw the leave type as unpaid, but there is no leave type named unpaid when we apply for a leave. so for the leavetypes that are not mentioned in the above, consider them as deduction and include that in the payroll generation. first analyze the complete workflow related to the employees and payroll before making any changes because this payroll is based on the employee salary and the leave they have applied/taken
**Date:** 2026-08-28  |  **Task ID:** 110  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- Backend: ASP.NET Core 6.0 (C#)
- Database: PostgreSQL (via Entity Framework Core)
- Frontend: MVC Razor Views + KnockoutJS, Tailwind CSS, Bootstrap, and FontAwesome

### Existing Modules & Features
- **Payroll Management** (`HRMS.Services/PayrollService.cs`): Computes employee payroll breakdowns, basic pay, allowances, leave-based deductions, absenteeism-based deductions, and net pay.
- **Leave Management** (`HRMS.Services/LeaveService.cs`): Records employee leaves with categories like "Sick Leave", "Casual Leave", "Annual Leave", "Maternity Leave", "Paternity Leave" and statuses like "Pending" or "Approved".
- **Employee Management** (`HRMS.Services/EmployeeService.cs`): Tracks employee base salaries and profile data, which directly feed into the payroll calculation.
- **Attendance Management** (`HRMS.Services/AttendanceService.cs`): Handles employee daily clock-in/clock-out records and absentee statuses.

### Prior Context
Analysis of prior contexts shows that payroll deductions are calculated by finding approved leave records within a specified month. The previous implementation checked if the leave type contains "Unpaid" (case-insensitive) before applying any deductions. However, there is no "Unpaid" category in the UI; hence, all leaves currently lead to zero deductions during payroll processing.

## Requirements Analysis

### Extracted Requirements
1. **Deduction Rule Realignment**: Redefine the leave deduction rules so that unless the leave category is Sick, Maternity, or Paternity, it must be considered as a deduction.
2. **Handle Correct Leave Categories**: Transition away from looking for "Unpaid" in the codebase, and instead treat "Casual Leave", "Annual Leave", and any other leave types not matching "Sick", "Maternity", or "Paternity" as deductible.
3. **Analyze & Maintain Accurate Computations**: Ensure leave deductions correctly calculate based on the employee's daily rate (calculated as `baseSalary / 22m`), matching the number of approved leave days overlapping with the payroll month.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Deduction Rule Realignment | Needs Modification | `HRMS.Services/PayrollService.cs` | Update leave type exclusion logic inside `GetPayrollBreakdownAsync`. |
| Handle Correct Leave Categories | Needs Modification | `HRMS.Services/PayrollService.cs` | Replace the check for "Unpaid" with a negative check for "sick", "maternity", and "paternity". |

## Tech Stack & Implementation

### Payroll Deduction Rule Adjustment — Needs Modification
- **Approach:** 
  In the `GetPayrollBreakdownAsync` method of `PayrollService.cs`, modify the iteration over `leaves` to change how deductible leaves are filtered. Instead of checking if `leave.LeaveType.Contains("Unpaid")`, verify if the leave type does NOT contain "sick", "maternity", or "paternity" (using case-insensitive comparison). If it is not one of these exempted categories, compute the overlap days for that leave and include them in the `leaveDays` tally for deduction.
- **Existing files to modify:** `HRMS.Services/PayrollService.cs`
- **New dependencies:** None

## Summary
The project is a mature brownfield ASP.NET Core and KnockoutJS application with fully developed employee, leave, and payroll processing capabilities. Currently, employees with approved leaves like "Casual Leave" or "Annual Leave" are receiving zero deductions during payroll generation because the service layer looks for a leave type named "Unpaid", which does not exist in the system's preset options.

This task requires modifying the existing backend business logic inside `PayrollService.cs` to change the leave deduction rules. Rather than white-listing "Unpaid", the code will be updated to black-list "Sick", "Maternity", and "Paternity" leaves. All other leave types will be classified as deductible, and their approved days will correctly reduce the calculated net pay. This modifies existing backend infrastructure without adding new files or third-party dependencies.
