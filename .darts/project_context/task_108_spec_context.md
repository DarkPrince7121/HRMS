# Spec Context — Task 108
**Generated:** 2026-08-28  |  **Framework:** KnockoutJS Backend ASP.NET Core 6.0  |  **Tasks:** 4

## Gap Analysis Summary
The existing application contains database structure for tracking Employee salaries (`BaseSalary` on the `Employee` entity), but the field has not been integrated into the user interface or DTO representation. As a result, payroll calculations default to 0. This task bridges that gap by implementing a non-negative salary input in the Employee Add/Edit modal, rendering base salary in the Employee Directory DataTable, and updating the payroll generator to calculate Gross Pay, Deductions, and Net Pay based on a newly introduced payroll breakdown structure.

## Task Plan

### Module: Employee Management

#### Feature: Employee Salary Field

**T-001: Implement Employee Salary — Backend Support & Validation**
- **Description:** Update employee backend models and service mapping to support `BaseSalary` transfer and input validation. Specifically:
  - Add `[Range(0, double.MaxValue, ErrorMessage = "Base salary must be a non-negative value.")]` validation attribute to `BaseSalary` in `HRMS.Models/DTOs/EmployeeRequest.cs`.
  - Add `BaseSalary` property (`public decimal BaseSalary { get; set; }`) to `HRMS.Models/DTOs/EmployeeResponse.cs`.
  - Update mapping inside `EmployeeService.cs` (`GetAllAsync`, `GetByIdAsync`, `GetByEmailAsync`) to assign `BaseSalary` from the `Employee` entity to `EmployeeResponse`.
- **Files to create:** None
- **Files to modify:** HRMS.Models/DTOs/EmployeeRequest.cs, HRMS.Models/DTOs/EmployeeResponse.cs, HRMS.Services/EmployeeService.cs
- **Depends on:** None
- **Acceptance criteria:**
  - Backend validation rejects negative `BaseSalary` values in `EmployeeRequest`.
  - `EmployeeResponse` correctly includes the `BaseSalary` retrieved from the database.
- **Wiring:**
  - Imports from: HRMS.Models/DTOs/EmployeeRequest.cs, HRMS.Models/DTOs/EmployeeResponse.cs
  - Imported by: HRMS.Services/EmployeeService.cs, HRMS.UI/Controllers/EmployeesController.cs
  - API routes: GET /Employees/GetAll, POST /Employees/Create, POST /Employees/Edit
  - DB tables: Employees
  - Env vars: None

**T-002: Implement Employee Salary — Frontend UI Integration**
- **Description:** Update the Employee Directory interface to show and manage the employee's base salary. Specifically:
  - Modify `HRMS.UI/Views/Employees/Index.cshtml` to add a "Base Salary" column to the `employeesTable` headers.
  - Modify `HRMS.UI/Views/Employees/Index.cshtml` to add a numeric input control inside the employee edit/add modal bound to `baseSalary` (e.g. `<input type="number" min="0" step="0.01" class="form-control bg-light border-0 py-2 shadow-none" data-bind="value: baseSalary" required />`).
  - Update `HRMS.UI/wwwroot/js/employees.js` to add the `baseSalary` column definition in the DataTable column specifications and format it as currency.
  - Add front-end validation in `saveEmployee()` inside `employees.js` to prevent submitting negative salaries.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Employees/Index.cshtml, HRMS.UI/wwwroot/js/employees.js
- **Depends on:** T-001
- **Acceptance criteria:**
  - Employee list DataTable displays a "Base Salary" column correctly formatted as currency (e.g. `$5,000.00`).
  - Add/Edit modals display a numeric input field for "Base Salary" that is bound to Knockout's `baseSalary` property.
  - Client-side validation rejects any negative numbers entered in the salary field.
- **Wiring:**
  - Imports from: HRMS.UI/wwwroot/js/employees.js
  - Imported by: HRMS.UI/Views/Employees/Index.cshtml
  - API routes: GET /Employees/GetAll, POST /Employees/Create, POST /Employees/Edit
  - DB tables: None
  - Env vars: None

---

### Module: Payroll Management

#### Feature: Payroll Calculation & Breakdown

**T-003: Implement Payroll Calculations & Breakdown — Backend Logic**
- **Description:** Implement detailed breakdown calculations inside the payroll service.
  - Establish a breakdown structure where Gross Pay comprises Basic Pay (70% of BaseSalary) and Allowances (30% of BaseSalary).
  - Deductions are compiled by breaking down Unpaid Leave days and Absenteeism days individually.
  - Refactor `PayrollService.cs` to calculate these breakdown metrics, ensuring `GrossPay`, `Deductions`, and `NetPay` are properly derived.
  - Update `PayrollResponse.cs` to contain these calculated breakdown fields: `BasicPay`, `Allowances`, `LeaveDeductions`, `AbsentDeductions`.
  - Map these fields inside `PayrollController.cs` in the `GetAll` action.
- **Files to create:** None
- **Files to modify:** HRMS.Services/PayrollService.cs, HRMS.Models/DTOs/PayrollResponse.cs, HRMS.UI/Controllers/PayrollController.cs
- **Depends on:** T-002
- **Acceptance criteria:**
  - Payroll generation correctly uses the employee's non-zero `BaseSalary` instead of defaulting to 0.
  - Calculated `GrossPay` equals `BasicPay + Allowances` (matching `BaseSalary`).
  - Total `Deductions` correctly sums up `LeaveDeductions` and `AbsentDeductions`.
  - `NetPay` is correctly calculated as `GrossPay - Deductions` (minimum 0).
- **Wiring:**
  - Imports from: HRMS.Models/DTOs/PayrollResponse.cs
  - Imported by: HRMS.UI/Controllers/PayrollController.cs
  - API routes: GET /Payroll/GetAll, POST /Payroll/Generate
  - DB tables: Payrolls, Employees, Leaves, Attendances
  - Env vars: None

**T-004: Display Payroll Breakdown in UI**
- **Description:** Update the Payroll list view to show the structured calculation breakdown to the user.
  - Modify `HRMS.UI/Views/Payroll/Index.cshtml` to add a "Breakdown" column header to the `payrollTable`.
  - Update `HRMS.UI/wwwroot/js/payroll.js` to add the `Breakdown` column to the DataTable column definitions.
  - The renderer for this column should construct a descriptive visual breakdown of the calculations (e.g. "Basic: $X | Allowances: $Y | Deductions: -$Z (Leaves: $L, Absences: $A)").
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Payroll/Index.cshtml, HRMS.UI/wwwroot/js/payroll.js
- **Depends on:** T-003
- **Acceptance criteria:**
  - The payroll table displays a new "Breakdown" column.
  - The "Breakdown" column renders a detailed, neat representation of the basic salary, allowances, and individual deduction sources.
- **Wiring:**
  - Imports from: HRMS.UI/wwwroot/js/payroll.js
  - Imported by: HRMS.UI/Views/Payroll/Index.cshtml
  - API routes: GET /Payroll/GetAll
  - DB tables: None
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Employee Management",
      "features": [
        {
          "feature": "Employee Salary Field",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Employee Salary — Backend Support & Validation",
              "description": "Update employee backend models and service mapping to support BaseSalary transfer and input validation.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.Models/DTOs/EmployeeRequest.cs",
                "HRMS.Models/DTOs/EmployeeResponse.cs",
                "HRMS.Services/EmployeeService.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Backend validation rejects negative BaseSalary values in EmployeeRequest.",
                "EmployeeResponse correctly includes the BaseSalary retrieved from the database."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models/DTOs/EmployeeRequest.cs",
                  "HRMS.Models/DTOs/EmployeeResponse.cs"
                ],
                "imported_by": [
                  "HRMS.Services/EmployeeService.cs",
                  "HRMS.UI/Controllers/EmployeesController.cs"
                ],
                "api_routes": [
                  "GET /Employees/GetAll",
                  "POST /Employees/Create",
                  "POST /Employees/Edit"
                ],
                "db_tables": [
                  "Employees"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-002",
              "name": "Implement Employee Salary — Frontend UI Integration",
              "description": "Update the Employee Directory interface to show and manage the employee's base salary.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Views/Employees/Index.cshtml",
                "HRMS.UI/wwwroot/js/employees.js"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "Employee list DataTable displays a 'Base Salary' column correctly formatted as currency.",
                "Add/Edit modals display a numeric input field for 'Base Salary' that is bound to Knockout's baseSalary property.",
                "Client-side validation rejects any negative numbers entered in the salary field."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.UI/wwwroot/js/employees.js"
                ],
                "imported_by": [
                  "HRMS.UI/Views/Employees/Index.cshtml"
                ],
                "api_routes": [
                  "GET /Employees/GetAll",
                  "POST /Employees/Create",
                  "POST /Employees/Edit"
                ],
                "db_tables": [],
                "env_vars": []
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Payroll Management",
      "features": [
        {
          "feature": "Payroll Calculation & Breakdown",
          "tasks": [
            {
              "id": "T-003",
              "name": "Implement Payroll Calculations & Breakdown — Backend Logic",
              "description": "Implement detailed breakdown calculations inside the payroll service, updating payroll mapping in the controller and DTO.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.Services/PayrollService.cs",
                "HRMS.Models/DTOs/PayrollResponse.cs",
                "HRMS.UI/Controllers/PayrollController.cs"
              ],
              "depends_on": [
                "T-002"
              ],
              "acceptance_criteria": [
                "Payroll generation correctly uses the employee's non-zero BaseSalary.",
                "Calculated GrossPay equals BasicPay + Allowances (matching BaseSalary).",
                "Total Deductions correctly sums up LeaveDeductions and AbsentDeductions.",
                "NetPay is correctly calculated as GrossPay - Deductions (minimum 0)."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models/DTOs/PayrollResponse.cs"
                ],
                "imported_by": [
                  "HRMS.UI/Controllers/PayrollController.cs"
                ],
                "api_routes": [
                  "GET /Payroll/GetAll",
                  "POST /Payroll/Generate"
                ],
                "db_tables": [
                  "Payrolls",
                  "Employees",
                  "Leaves",
                  "Attendances"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-004",
              "name": "Display Payroll Breakdown in UI",
              "description": "Update the Payroll list view to show the structured calculation breakdown to the user.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Views/Payroll/Index.cshtml",
                "HRMS.UI/wwwroot/js/payroll.js"
              ],
              "depends_on": [
                "T-003"
              ],
              "acceptance_criteria": [
                "The payroll table displays a new 'Breakdown' column.",
                "The 'Breakdown' column renders a detailed, neat representation of the basic salary, allowances, and individual deduction sources."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.UI/wwwroot/js/payroll.js"
                ],
                "imported_by": [
                  "HRMS.UI/Views/Payroll/Index.cshtml"
                ],
                "api_routes": [
                  "GET /Payroll/GetAll"
                ],
                "db_tables": [],
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
