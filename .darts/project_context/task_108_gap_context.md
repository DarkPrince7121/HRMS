# Gap Context — I can see a column named Salary in the employees table, but there is no input control to add data to it, so because of that, the payroll generation results in 0, so add one more input control in the employees edit/add page and and display the salary value in the employees list page table as well. then this salary should be non-negative value. and also during payroll generation, the grosspay, deduction and netpay should be calculated based on the salary and it should have some breakdown structure. this should not break any other page
**Date:** 2026-08-28  |  **Task ID:** 108  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 6.0, Entity Framework Core
- **Frontend:** KnockoutJS, jQuery, Bootstrap, DataTables.net
- **Database:** Relational database managed through EF Core Migrations

### Existing Modules & Features
- **Employee Directory** (`HRMS.Models/Entities/Employee.cs`, `HRMS.Services/EmployeeService.cs`, `HRMS.UI/Controllers/EmployeesController.cs`, `HRMS.UI/Views/Employees/Index.cshtml`, `HRMS.UI/wwwroot/js/employees.js`): Allows viewing, adding, editing, and deleting employee records. The backend entity holds `BaseSalary`, but the UI lacks an input field, and the service maps to `EmployeeResponse` without including the salary, causing it to display as 0 or not display at all.
- **Payroll Management** (`HRMS.Models/Entities/Payroll.cs`, `HRMS.Services/PayrollService.cs`, `HRMS.UI/Controllers/PayrollController.cs`, `HRMS.UI/Views/Payroll/Index.cshtml`, `HRMS.UI/wwwroot/js/payroll.js`): Generates payroll records monthly, calculating gross pay, deductions (based on unpaid leave days and absent days), and net pay. Because employee salaries default to 0, current payroll generations calculate all pays as 0.

### Prior Context
No prior analysis found for this project.

## Requirements Analysis

### Extracted Requirements
1. **Add Salary Input Control:** Provide an input control in the Add and Edit Employee modals on the Employee screen to manage the employee's salary.
2. **Display Salary in List Page:** Display the Salary value as a dedicated column in the Employees DataTable list.
3. **Salary Validation:** Enforce that the salary value must be a non-negative value (greater than or equal to 0) in both the frontend (KnockoutJS validation / input constraints) and backend models.
4. **Payroll Calculation Based on Salary:** Ensure that payroll generation utilizes the entered `BaseSalary` to correctly compute Gross Pay, Deductions, and Net Pay.
5. **Breakdown Structure for Payroll:** Establish a structured calculation breakdown (such as separating basic pay, standard allowances, and attendance-based/leave-based deductions) during payroll computation and generation, and optionally surface these breakdown components.
6. **No Regression:** Ensure these changes do not break other pages or features (such as attendance, dashboard, etc.).

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Add Salary Input Control | Needs Modification | `HRMS.UI/Views/Employees/Index.cshtml`, `HRMS.UI/wwwroot/js/employees.js` | Introduce field bounded to Knockout observable `baseSalary`. |
| Display Salary in List Table | Needs Modification | `HRMS.Models/DTOs/EmployeeResponse.cs`, `HRMS.Services/EmployeeService.cs`, `HRMS.UI/Views/Employees/Index.cshtml`, `HRMS.UI/wwwroot/js/employees.js` | Add `BaseSalary` to the response DTO, map it in the service layer, add table header/column in DataTable initialization. |
| Salary Validation | Needs Modification | `HRMS.Models/DTOs/EmployeeRequest.cs`, `HRMS.UI/Views/Employees/Index.cshtml`, `HRMS.UI/wwwroot/js/employees.js` | Add backend Validation Attribute `[Range(0, double.MaxValue)]` or equivalent, and enforce HTML5 `min="0"` with Knockout validation check before submitting. |
| Payroll Calculation & Breakdown | Needs Modification | `HRMS.Services/PayrollService.cs`, `HRMS.Models/Entities/Payroll.cs`, `HRMS.Models/DTOs/PayrollResponse.cs`, `HRMS.UI/Views/Payroll/Index.cshtml`, `HRMS.UI/wwwroot/js/payroll.js` | Update calculation logic to split salary into specific structures (e.g. Basic pay, Allowances, Deductions) and show/store this breakdown structured format. |

## Tech Stack & Implementation

### Add & Display Employee Salary — Needs Modification
- **Approach:**
  - Update `EmployeeResponse.cs` to expose `BaseSalary`.
  - Update mapping in `EmployeeService.cs` (`GetAllAsync`, `GetByIdAsync`, `GetByEmailAsync`) to assign `BaseSalary` to `EmployeeResponse`.
  - Update `HRMS.UI/Views/Employees/Index.cshtml` by adding a "Salary" column header to the `employeesTable` and adding a numeric input control bound to `baseSalary` inside the employee modal.
  - Update `HRMS.UI/wwwroot/js/employees.js` to include the `baseSalary` field in DataTable column definitions, form resetting, and JSON payload construction.
- **Existing files to modify:** 
  - `HRMS.Models/DTOs/EmployeeResponse.cs`
  - `HRMS.Services/EmployeeService.cs`
  - `HRMS.UI/Views/Employees/Index.cshtml`
  - `HRMS.UI/wwwroot/js/employees.js`
- **New dependencies:** None

### Salary Validation — Needs Modification
- **Approach:**
  - Apply `[Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]` on `BaseSalary` property in `EmployeeRequest.cs`.
  - Add client-side validation in `employees.js` (or inline validation alerts) to ensure that the user cannot submit negative numbers, and use standard HTML `type="number" min="0" step="0.01"` inside `Index.cshtml`.
- **Existing files to modify:**
  - `HRMS.Models/DTOs/EmployeeRequest.cs`
  - `HRMS.UI/Views/Employees/Index.cshtml`
  - `HRMS.UI/wwwroot/js/employees.js`
- **New dependencies:** None

### Payroll Calculation with Breakdown — Needs Modification
- **Approach:**
  - Refactor `PayrollService.cs` to implement a structured breakdown calculation. For example, structure the calculation where `GrossPay` consists of a `BasicSalary` (e.g., 70% of BaseSalary) plus `Allowances` (e.g., 30% of BaseSalary). Calculate the leave/absenteeism-based `Deductions` relative to this base salary.
  - Extend the `PayrollResponse` DTO and `Payroll` entity if necessary (or return structured breakdown notes) to cleanly represent how the Gross Pay, Deductions, and Net Pay are compiled.
  - Render these breakdown details (e.g., inside a sub-view, custom column, or structured tooltip) on the `payrollTable` or as additional UI descriptors.
- **Existing files to modify:**
  - `HRMS.Services/PayrollService.cs`
  - `HRMS.Models/Entities/Payroll.cs`
  - `HRMS.Models/DTOs/PayrollResponse.cs`
  - `HRMS.UI/Views/Payroll/Index.cshtml`
  - `HRMS.UI/wwwroot/js/payroll.js`
- **New dependencies:** None

## Summary
The current project is a mature, brownfield HRMS application built on ASP.NET Core and KnockoutJS. Although the backend entities are equipped to track and store an employee's base salary, the frontend directory views lack any interface controls to display or input these salaries. Consequently, during monthly payroll generations, salaries default to zero, rendering all calculations for Gross Pay, Deductions, and Net Pay as zero.

To address this, we must enable inputting, validating, and presenting the base salary on the Employees Directory page. We will modify the employee list's DataTable UI, form bindings, and backend DTOs to support this field securely. Furthermore, the payroll generation service must be updated to apply an explicit calculation breakdown structure based on the newly available salary values. 

The implementation approach is highly targeted and additive. It refines existing CRUD mechanisms and calculations without introducing new dependencies or database schema overhauls, ensuring system stability across all other dashboard and management modules.
