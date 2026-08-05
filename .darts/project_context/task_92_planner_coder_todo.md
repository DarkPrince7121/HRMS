# Planner-Coder Todo — 92
**Requirement:** Register is still not working completely. Register page is displayed, after entering the username, email & password, on clicking Register, there is nothing happened, but the data entered should be added in the database. Please find the root cause and fix it.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Program.cs: ControllersWithViews, DbContext, Authentication, Authorization, Routing, Default Route
- HRMS.UI/Views/Account/Register.cshtml: KnockoutJS, auth.js, RegisterViewModel, form submit: register

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Controllers/AccountController.cs: Fixed Register action to handle missing roles and potential validation issues.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Fix AccountController Register logic | HRMS.UI/Controllers/AccountController.cs | pending | — |
