# Planner-Coder Todo — 90
**Requirement:** In the Login Page, add link to register new user, and make changes accordingly. If needed add migration and apply it.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.UI/Controllers/AccountController.cs: Login, Logout, CookieAuthenticationDefaults
- HRMS.UI/Views/Account/Login.cshtml: Login form, knockout bindings
- HRMS.UI/wwwroot/js/auth.js: LoginViewModel

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.UI/Controllers/AccountController.cs: add Register action (GET and POST)
- HRMS.UI/Views/Account/Register.cshtml: create new view with registration form
- HRMS.UI/Views/Account/Login.cshtml: add link to /Account/Register
- HRMS.UI/wwwroot/js/auth.js: add RegisterViewModel, switch logic for login/register page
- HRMS.Models/DTOs/RegisterRequest.cs: create new DTO

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Register DTO & Controller Updates | HRMS.Models/DTOs/RegisterRequest.cs, HRMS.UI/Controllers/AccountController.cs | pending | — |
| T-002 | Frontend — Register View & UI Updates | HRMS.UI/Views/Account/Register.cshtml, HRMS.UI/Views/Account/Login.cshtml, HRMS.UI/wwwroot/js/auth.js | pending | T-001 |
