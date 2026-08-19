# Planner-Coder Todo — 96
**Requirement:** On loading/navigating to most of the pages, I'm getting the below error in the browser console
jquery-3.6.0.min.js:2 Uncaught Error: You cannot apply bindings multiple times to the same element.

Verify all the cshtml & their respective viewmodel file an fix all the binding issue and as an user, i should not get any error for binding in the browser console for any page.
Analyze the root cause and fix the issue and also the fix that you have provided for the department page is still not yet fixed

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Views/Shared/_Layout.cshtml: KnockoutJS, jquery, bootstrap, notifications.js, RenderSectionAsync("Scripts")
- HRMS.UI/wwwroot/js/notifications.js: NotificationViewModel, ko.applyBindings(..., document.getElementById('notificationDropdown'))

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Views/Shared/_Layout.cshtml: Wrap the unread count and notification list in a container with id="notificationArea" and update notifications.js to bind to that specific element.
- HRMS.UI/wwwroot/js/notifications.js: Update binding to document.getElementById('notificationArea').
- HRMS.UI/wwwroot/js/*.js: Remove all auto-initialization `$(document).ready(function() { ko.applyBindings(...) })` blocks from page-specific JS files.
- HRMS.UI/Views/**/*.cshtml: Explicitly call `ko.applyBindings(new ViewModel(), document.getElementById('pageContent'))` in the @section Scripts for each page, and ensure each page's content is wrapped in a div with id="pageContent".

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Fix Layout and Global Notifications Binding | HRMS.UI/Views/Shared/_Layout.cshtml, HRMS.UI/wwwroot/js/notifications.js | pending | — |
| T-002 | Clean up Page-Specific ViewModels (Remove Auto-binding) | HRMS.UI/wwwroot/js/attendance.js, HRMS.UI/wwwroot/js/audit.js, HRMS.UI/wwwroot/js/dashboard.js, HRMS.UI/wwwroot/js/departments.js, HRMS.UI/wwwroot/js/designations.js, HRMS.UI/wwwroot/js/employees.js, HRMS.UI/wwwroot/js/holidays.js, HRMS.UI/wwwroot/js/leaves.js, HRMS.UI/wwwroot/js/payroll.js, HRMS.UI/wwwroot/js/settings.js | pending | T-001 |
| T-003 | Update Views to use Targeted Bindings | HRMS.UI/Views/Attendance/Index.cshtml, HRMS.UI/Views/Audit/Index.cshtml, HRMS.UI/Views/Home/Index.cshtml, HRMS.UI/Views/Departments/Index.cshtml, HRMS.UI/Views/Designations/Index.cshtml, HRMS.UI/Views/Employees/Index.cshtml, HRMS.UI/Views/Holidays/Index.cshtml, HRMS.UI/Views/Leave/Index.cshtml, HRMS.UI/Views/Payroll/Index.cshtml, HRMS.UI/Views/Settings/Index.cshtml | pending | T-002 |
