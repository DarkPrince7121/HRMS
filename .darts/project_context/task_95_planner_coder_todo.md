# Planner-Coder Todo — 95
**Requirement:** On navigating to the Departments Page, I'm getting the below error message in the console
jquery-3.6.0.min.js:2
 Uncaught Error: You cannot apply bindings multiple times to the same element.

I checked the viewmodel file, in that... instead of apply the binding in the viewmodel, can you please do that in the cshtml level

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Departments\Index.cshtml: ko.applyBindings already in Scripts section.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\departments.js: No applyBindings found in the current file read.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Departments\Index.cshtml: Ensure ko.applyBindings is correctly called at the view level (which it currently is).
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\departments.js: Remove any existing applyBindings call (if found in full file search).

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Cleanup ViewModel and verify View binding | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\departments.js, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Departments\Index.cshtml | pending | — |
