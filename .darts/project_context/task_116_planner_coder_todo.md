# Planner-Coder Todo — 116
**Requirement:** Implement the general KnockoutJS list view for withdrawal applications, including a global navigation item, list table rendering, and an entry button to launch the multi-step application wizard flow.

Acceptance Criteria:
- New global application layout or sidebar contains nav link 'Applications'
- Clicking 'Applications' navigates to the table list page without browser-wide refresh, rendering a table listing all existing withdrawal applications with their metadata (ID, Status, Amount, Target Employee, Dates)
- Add a 'Create New Withdrawal Application' button on the page that initiates the 4-step application wizard

Technical Hints: Define a new KnockoutJS route or component for the applications list. Use standard data-bind templates to render rows and status badges.

Dependencies: Task backend-api-application-wizard

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- `HRMS.UI/Views/Shared/_Layout.cshtml`: global navigation bar with existing tabs (Leaves, Payroll, Directory)

### Planned (add exactly these in STEP 3)
- `HRMS.UI/Views/Shared/_Layout.cshtml`: add "Applications" navigation link with SPA click action
- `HRMS.UI/Controllers/ApplicationsController.cs`: add withdrawal application initiate, status, and completion endpoints
- `HRMS.UI/Views/Applications/Index.cshtml`: the template for the applications list and the multi-step wizard
- `HRMS.UI/wwwroot/js/applications.js`: the KnockoutJS ViewModel code and routing controller

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend application wizard endpoint additions | `HRMS.UI/Controllers/ApplicationsController.cs` | pending | — |
| T-002 | Navigation integration in _Layout.cshtml | `HRMS.UI/Views/Shared/_Layout.cshtml` | pending | T-001 |
| T-003 | KnockoutJS applications and wizard services | `HRMS.UI/wwwroot/js/applications.js` | pending | T-002 |
| T-004 | Frontend HTML view and wizard UI components | `HRMS.UI/Views/Applications/Index.cshtml` | pending | T-003 |
