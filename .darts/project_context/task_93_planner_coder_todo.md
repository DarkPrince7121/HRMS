# Planner-Coder Todo — 93
**Requirement:** On clicking the Register button after entering the details in the input fields of the Register page, Getting the below error in the browser console:
1. `knockout-latest.js:79 Uncaught ReferenceError: Unable to process binding "visible: function(){return successMessage }"`
2. `Register:40 Uncaught ReferenceError: RegisterViewModel is not defined`

Analyze the root cause and fix it.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Account\Register.cshtml: imports knockout.js, auth.js, applies bindings to RegisterViewModel.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\auth.js: defines LoginViewModel and RegisterViewModel.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Account\Register.cshtml: Fix the order of script execution or initialization.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\auth.js: Ensure RegisterViewModel is correctly exported/available.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Frontend UI — Fix Register page script execution and ViewModel definition | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Views\Account\Register.cshtml, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\wwwroot\js\auth.js | pending | — |
