# Gap Context — Implement the 4-step KnockoutJS application wizard UI flow (Instructions, User Selection, Amount Entering, and Application Submitted) dynamically handling state, back/next navigation, validation of remaining balances, and API integrations.

Acceptance Criteria:
- Instructions step shows 100+ words of descriptive process text and a dynamic action button (e.g., 'Start Application' or 'Continue Application')
- User Selection step renders a dropdown populated with employees, showing Full Name, Email, Department, and Salary below upon selection. Includes 'Back' and 'Next' buttons
- Amount Entering step displays total processed salary, calculated pending deduction, lets user input withdrawal amount, shows real-time final balance remaining. Includes 'Back' and 'Next' buttons
- Application Submitted step displays confirmation layout, application ID, target employee details, finalized amount, and a 'Complete Application' button that posts the completion, updates state, and redirects to dashboard
- State transitions successfully update StatusId in database via api calls at each wizard progression

Technical Hints: Create a view-model containing wizard page index, selectedEmployee observable, balance calculations, and ajax calls for each wizard step. Handle navigation state properly so users cannot proceed to the next page without valid step inputs (e.g., selection made or positive amount entered).

Dependencies: Task frontend-applications-list
**Date:** 2026-09-02  |  **Task ID:** 117  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 6.0 MVC Web Application & API controllers
- **Frontend:** KnockoutJS, Bootstrap 5, jQuery (AJAX), HTML5, Razor Views
- **Database:** PostgreSQL via Entity Framework Core

### Existing Modules & Features
- **Applications Controller & Views** (`HRMS.UI/Controllers/ApplicationsController.cs`, `HRMS.UI/Views/Applications/Index.cshtml`): Handles MVC rendering and API endpoints for listing (`/api/applications`), initiating (`/api/applications/initiate`), updating status (`/api/applications/{id}/status`), and completing applications (`/api/applications/{id}/complete`).
- **Employees API Controller** (`HRMS.UI/Controllers/EmployeesApiController.cs`): Provides endpoint to fetch all active employees with their details (`/api/employees`) and check real-time balances/pending deductions (`/api/employees/{id}/balance`).
- **Applications Javascript ViewModel** (`HRMS.UI/wwwroot/js/applications.js`): Manages the KnockoutJS client-side wizard flows, subscription handling, step navigation, and backend synchronization.

### Prior Context
No prior analysis found for this specific task. The existing solution contains structured files for database models, web routing, and client bindings but is partially incomplete regarding specific wizard UX validations, fields, buttons, and redirect behaviors.

## Requirements Analysis

### Extracted Requirements
1. **Instructions Step Enhancement:**
   - Display a descriptive instructions text exceeding 100 words. (Currently matches this word count in HTML layout).
   - Render a dynamic action button depending on state: "Start Application" (if new application) or "Continue Application" (if resumed in-progress application) instead of a simple "Next" button on Step 1.
2. **User Selection Step Details:**
   - Render a dropdown populated with active employees.
   - Upon selecting an employee, show their Full Name, Email, Department, and Base Salary details beneath the selector.
   - Include functional "Back" and "Next" buttons with appropriate validation block.
3. **Amount Entering Step Enhancements:**
   - Display the total processed salary (`AmountBalance`), the calculated pending deduction (`PendingWithdrawals`), allow a numeric withdrawal amount input, and show a real-time final balance remaining (`NetBalance - EnteredAmount`).
   - Validate input: block the "Next" button if the entered amount is non-positive or exceeds the employee's available net salary balance.
4. **Application Submitted Step Details:**
   - Display a robust confirmation layout presenting Application ID, target employee details, and finalized withdrawal amount.
   - Include a "Complete Application" button that sends a POST to finalize state on the backend.
   - Redirect dynamically to the dashboard page (`/Home/Index` or `/`) upon a successful completion response.
5. **Database State & Flow Sync:**
   - Ensure back-and-forth wizard progressions trigger PUT calls to `api/applications/{id}/status` keeping database `StatusId` in complete sync.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Instructions text (100+ words) | Already Exists | `HRMS.UI/Views/Applications/Index.cshtml` | Currently has ~106 words of instructional text. |
| Dynamic action button in Step 1 | Needs Modification | `HRMS.UI/Views/Applications/Index.cshtml`, `HRMS.UI/wwwroot/js/applications.js` | Update button text dynamically depending on whether application is being resumed or started fresh. |
| Show selected employee Full Name, Email, Department, and Salary | Needs Modification | `HRMS.UI/Views/Applications/Index.cshtml`, `HRMS.UI/wwwroot/js/applications.js` | Expand bindings to capture and display department and base salary fields returned by `/api/employees`. |
| Amount Entering step balance & pending calculation displays | Needs Modification | `HRMS.UI/Views/Applications/Index.cshtml`, `HRMS.UI/wwwroot/js/applications.js` | Enhance ViewModel and markup to present processed salary, pending deductions, and calculated real-time remaining balance (`netBalance - enteredAmount`). |
| Submit Confirmation with 'Complete Application' button & redirect | Needs Modification | `HRMS.UI/Views/Applications/Index.cshtml`, `HRMS.UI/wwwroot/js/applications.js` | Correct button text, ensure correct post action, update local state, and trigger `window.location.href` redirect to Dashboard. |
| State transitions update `StatusId` via APIs | Already Exists | `HRMS.UI/wwwroot/js/applications.js`, `HRMS.UI/Controllers/ApplicationsController.cs` | Logic is mostly implemented but requires validation mapping to guard next transitions. |

## Tech Stack & Implementation

### Wizard Navigation, Display and Redirects — Needs Modification
- **Approach:**
  - **Step 1 Action Button:** Introduce a Knockout computed observable or subscription tracking whether the current application was freshly created or resumed. Bind this text to the primary action button in Step 1 (e.g., "Start Application" vs "Continue Application").
  - **Step 2 Details:** Expose `selectedEmployeeDepartment` and `selectedEmployeeSalary` as observables on the ViewModel. Ensure that when an employee selection is made, these properties are populated from the matching employee object in `employees` array. Update the markup in the view to render these attributes dynamically.
  - **Step 3 Balance Fields:** Introduce computed observables for calculated pending deduction, total processed salary, and real-time final balance remaining. These will calculate dynamically on keyup/change of the withdrawal amount input.
  - **Step 4 Complete Button & Redirect:** Update the text of the finalize button to "Complete Application". Implement a client-side route redirection `window.location.href = '/'` or `/Home/Index` in the successful AJAX handler of `completeApplication`.
- **Existing files to modify:**
  - `HRMS.UI/Views/Applications/Index.cshtml`
  - `HRMS.UI/wwwroot/js/applications.js`
- **New dependencies:** None

## Summary
The project is a mature **Brownfield** ASP.NET Core web application using a PostgreSQL database and a multi-tiered architecture (Entities, Services, Controllers). The views utilize KnockoutJS for reactive single-page client bindings and dynamic modal interaction. 

The task requires extending the KnockoutJS-driven Withdrawal Wizard by modifying the UI markup inside the Razor view `Index.cshtml` and implementing proper state handling in the script file `applications.js`. The objective is to make the wizard dynamic, descriptive, informative, fully validated against employee balances, and cohesive with system navigation by redirecting to the dashboard on completion. 

The overall implementation character is additive/modifying existing user interface files, requiring no changes to the backend APIs or DB schema, which already support these functions.
