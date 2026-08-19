# Planner-Coder Todo — 98
**Requirement:** As an user on navigating to the Payroll page, I'm getting this datatable alert 
DataTables warning: table id=payrollTable - Cannot reinitialise DataTable. For more information about this error, please see http://datatables.net/tn/3

and on confirming that, I'm getting this same error in my browser console
jquery-3.6.0.min.js:2 Uncaught Error: You cannot apply bindings multiple times to the same element.

Please find the root cause of these both issue and fx it

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Views/Payroll/Index.cshtml: Scripts section with ko.applyBindings
- HRMS.UI/wwwroot/js/payroll.js: PayrollViewModel definition

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Views/Payroll/Index.cshtml: Ensure ko.applyBindings is safe and DataTable initialization doesn't double-fire.
- HRMS.UI/wwwroot/js/payroll.js: Remove auto-init of DataTable inside constructor to allow controlled initialization.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Frontend UI — Fix double binding and DataTable reinitialization | HRMS.UI/Views/Payroll/Index.cshtml, HRMS.UI/wwwroot/js/payroll.js | pending | — |
