# Planner-Coder Todo — 106
**Requirement:** On navigating to Leave page, I'm getting error and on clicking the request leave, in the pop up dialog the employees dropdown is not displaying results.
This was the error i got in the console: jquery-3.6.0.min.js:2
 Uncaught TypeError: Cannot read properties of undefined (reading 'style')

Find the root cause and fix it so the user can able to request leave without any error !

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Views/Leave/Index.cshtml: ko.applyBindings(new LeaveViewModel(), el)
- HRMS.UI/wwwroot/js/leaves.js: LeaveViewModel, loadEmployees, initDataTable
- HRMS.UI/wwwroot/js/notifications.js: NotificationViewModel, ko.applyBindings(notificationVm, notificationArea)

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/wwwroot/js/leaves.js: Fix error by ensuring `table` is initialized before use or check if defined.
- HRMS.UI/Views/Leave/Index.cshtml: Ensure `pageContent` binding doesn't conflict with `notificationArea`.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Fix JavaScript errors and Dropdown data loading | HRMS.UI/wwwroot/js/leaves.js, HRMS.UI/Views/Leave/Index.cshtml | pending | — |
