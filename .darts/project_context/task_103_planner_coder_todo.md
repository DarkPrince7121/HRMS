# Planner-Coder Todo — 103
**Requirement:** Bridge the gap between Users and Employees. When a user logs in, check if an employee record exists with the same email. If not, redirect to a registration page for employee details (First Name, Last Name, Department - default IT). Add a UserId foreign key to the Employees table. Ensure email address in Employees is unique. Use partial views for the employee form.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- HRMS.Data/ApplicationDbContext.cs: DbSet<Employee>, DbSet<User>, modelBuilder.ApplyConfiguration(new EmployeeConfiguration())
- HRMS.UI/Program.cs: builder.Services.AddScoped<IEmployeeService, EmployeeService>
- HRMS.UI/Controllers/AccountController.cs: Login and Register actions
- HRMS.UI/Controllers/EmployeesController.cs: Create action
- HRMS.Services/EmployeeService.cs: CreateAsync, GetByIdAsync

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.Data/ApplicationDbContext.cs: (Entities already registered, just updating schema in Entities)
- HRMS.UI/Controllers/AccountController.cs: add `RegisterEmployee` (GET/POST), modify `Login` to check for employee existence.
- HRMS.Services/Interfaces/IEmployeeService.cs: add `Task<EmployeeResponse?> GetByEmailAsync(string email);`
- HRMS.Services/EmployeeService.cs: implement `GetByEmailAsync`.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — ALL models + ALL controllers + DbContext updates | HRMS.Models/Entities/Employee.cs, HRMS.Models/DTOs/EmployeeRequest.cs, HRMS.Data/Configurations/EmployeeConfiguration.cs, HRMS.Services/Interfaces/IEmployeeService.cs, HRMS.Services/EmployeeService.cs, HRMS.UI/Controllers/AccountController.cs, HRMS.UI/Controllers/EmployeesController.cs | pending | — |
| T-002 | Frontend UI — ALL components + CSS + Views | HRMS.UI/Views/Shared/_EmployeeFormPartial.cshtml, HRMS.UI/Views/Account/RegisterEmployee.cshtml, HRMS.UI/wwwroot/js/auth.js | pending | T-001 |
