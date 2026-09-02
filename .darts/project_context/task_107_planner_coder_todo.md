# Planner-Coder Todo — 107
**Requirement:** Fix "Uncaught TypeError: Cannot read properties of undefined (reading 'style')" on Leave page and ensure similar implementation as fixed pages (ViewModel and DataTable). Enable leave application, viewing, and admin approval.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Views/Leave/Index.cshtml: KnockoutJS binding in @section Scripts.
- HRMS.UI/wwwroot/js/leaves.js: LeaveViewModel implementation.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/wwwroot/js/leaves.js: standardizing DataTable initialization and data handling.
- HRMS.UI/Views/Leave/Index.cshtml: fixing Knockout binding container and section structure.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Frontend UI — Fix Leave page JS and CSHTML | HRMS.UI/wwwroot/js/leaves.js, HRMS.UI/Views/Leave/Index.cshtml | pending | — |
