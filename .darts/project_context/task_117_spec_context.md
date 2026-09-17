# Spec Context — Task 117
**Generated:** 2026-09-02  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 1

## Gap Analysis Summary
The repository contains a fully structured brownfield ASP.NET Core web application using PostgreSQL, Entity Framework Core, and KnockoutJS. The existing wizard UI flow in `HRMS.UI/Views/Applications/Index.cshtml` and state binding in `HRMS.UI/wwwroot/js/applications.js` handles some navigation and basic validation, but is incomplete. Specifically, it lacks dynamic button labeling on Step 1, additional employee profile displays (Full Name, Email, Department, and Salary) on Step 2, advanced balance calculations (Processed Salary, Pending Deductions, and final remaining balance) on Step 3, proper confirmation layout labels on Step 4, and dashboard redirection upon successful POST completion. 

## Task Plan

### Module: Withdrawal Management

#### Feature: Withdrawal Wizard Workflow

**T-001: Implement 4-step KnockoutJS application wizard UI flow**
- **Description:** Enhance the client-side single-page KnockoutJS Wizard by adding advanced balance observables, dynamic start/continue button states, detailed target employee info displays, real-time pending balance deductions calculations, and a post-submission redirect to the dashboard.
- **Files to create:** None
- **Files to modify:** HRMS.UI/Views/Applications/Index.cshtml, HRMS.UI/wwwroot/js/applications.js
- **Depends on:** None
- **Acceptance criteria:**
  - Step 1 (Instructions) shows 100+ words of descriptive process text and a dynamic action button displaying "Continue Application" if resuming or "Start Application" if starting fresh.
  - Step 2 (User Selection) displays employee dropdown and, upon selection, dynamically shows Full Name, Email, Department, and Salary. "Back" and "Next" buttons navigate and block progression if no employee is selected.
  - Step 3 (Amount Entering) displays total processed salary, calculated pending deductions, input amount field, and real-time remaining balance (`netBalance - enteredAmount`). "Next" button validates that the entered amount is positive and does not exceed the net balance.
  - Step 4 (Application Submitted) displays detailed confirmation layout with Application ID, selected employee name, finalized amount, and a "Complete Application" button.
  - Clicking "Complete Application" posts state to the backend, closes the modal, and triggers a full redirect to the dashboard home page (`/`).
  - Moving back and forth through wizard steps triggers HTTP PUT requests to `/api/applications/{id}/status` to synchronize application state in PostgreSQL.
- **Wiring:**
  - Imports from: None
  - Imported by: None
  - API routes:
    - GET /api/applications
    - POST /api/applications/initiate
    - PUT /api/applications/{id}/status
    - POST /api/applications/{id}/complete
    - GET /api/employees
    - GET /api/employees/{id}/balance
  - DB tables: WithdrawalApplications, Employees, EmployeeSalaryBalances
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Withdrawal Management",
      "features": [
        {
          "feature": "Withdrawal Wizard Workflow",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement 4-step KnockoutJS application wizard UI flow",
              "description": "Enhance the client-side single-page KnockoutJS Wizard by adding advanced balance observables, dynamic start/continue button states, detailed target employee info displays, real-time pending balance deductions calculations, and a post-submission redirect to the dashboard.",
              "files_to_create": [],
              "files_to_modify": [
                "HRMS.UI/Views/Applications/Index.cshtml",
                "HRMS.UI/wwwroot/js/applications.js"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Step 1 (Instructions) shows 100+ words of descriptive process text and a dynamic action button displaying 'Continue Application' if resuming or 'Start Application' if starting fresh.",
                "Step 2 (User Selection) displays employee dropdown and, upon selection, dynamically shows Full Name, Email, Department, and Salary. 'Back' and 'Next' buttons navigate and block progression if no employee is selected.",
                "Step 3 (Amount Entering) displays total processed salary, calculated pending deductions, input amount field, and real-time remaining balance (netBalance - enteredAmount). 'Next' button validates that the entered amount is positive and does not exceed the net balance.",
                "Step 4 (Application Submitted) displays detailed confirmation layout with Application ID, selected employee name, finalized amount, and a 'Complete Application' button.",
                "Clicking 'Complete Application' posts state to the backend, closes the modal, and triggers a full redirect to the dashboard home page ('/').",
                "Moving back and forth through wizard steps triggers HTTP PUT requests to '/api/applications/{id}/status' to synchronize application state in PostgreSQL."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [],
                "imported_by": [],
                "api_routes": [
                  "GET /api/applications",
                  "POST /api/applications/initiate",
                  "PUT /api/applications/{id}/status",
                  "POST /api/applications/{id}/complete",
                  "GET /api/employees",
                  "GET /api/employees/{id}/balance"
                ],
                "db_tables": [
                  "WithdrawalApplications",
                  "Employees",
                  "EmployeeSalaryBalances"
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
