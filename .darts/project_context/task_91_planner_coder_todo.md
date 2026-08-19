# Planner-Coder Todo — 91
**Requirement:** After entering the data in the register page, and clicking the register, the user data is not added in the users table. Please check the root cause and fix it.,
And along with that, in login, if the user provided username & password, after getting the user from the database, the passwordhash verification is performed, but the problem here is, the password entered by the user is not hashed before checking with the existing hash, so please fix that as well.
before starting the both, please go through the flow of both Register & Login functionality and find the gap

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\AccountController.cs: AccountController using IPasswordHasher<User>
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: Cookie authentication and database initialization registered.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- None (fixing existing implementation in AccountController.cs)

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Fix Login/Register backend logic | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Controllers\AccountController.cs | pending | — |
