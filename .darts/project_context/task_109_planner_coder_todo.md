# Planner-Coder Todo — 109
**Requirement:** On loading the employees page, I'm getting the below error in the console 
28Tracking Prevention blocked access to storage for <URL>.
employees.js:47 Uncaught SyntaxError: Invalid or unexpected token (at employees.js:47:32)
jquery-3.6.0.min.js:2 jQuery.Deferred exception: EmployeeViewModel is not defined ReferenceError: EmployeeViewModel is not defined
    at HTMLDocument.<anonymous> (https://localhost:53587/Employees:341:38)
   
jquery-3.6.0.min.js:2 Uncaught ReferenceError: EmployeeViewModel is not defined
(anonymous) @ Employees:341

and there is no data loaded in the table, find the root cause of this issue and fix this, 
and also on loading the payroll page works fine but this employees page gives error

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.UI\wwwroot\js\employees.js: Existing EmployeeViewModel definition and table initialization.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.UI\wwwroot\js\employees.js: Clean and rebuild the JavaScript file to correct syntax error in baseSalary column renderer and remove corrupted duplicate blocks.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Fix employees.js syntax error | C:\DARTS-development-environment\sandbox\rmanoj\HRMS-Phase-1\HRMS.UI\wwwroot\js\employees.js | pending | — |
