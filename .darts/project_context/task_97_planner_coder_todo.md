# Planner-Coder Todo — 97
**Requirement:** As an user, If i try to create a new attendance or leave request, on submitting the form pop up with date field in it, I'm getting the below error 500
Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
An unhandled exception has occurred while executing the request.
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
 ---> System.InvalidCastException: Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array/range. See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
   at Npgsql.Internal.TypeHandlers.DateTimeHandlers.TimestampTzHandler.ValidateAndGetLength(DateTime value, NpgsqlParameter parameter)


Check all the pages with input form that contains date/datetime field in it and find the root cause of this issue and fix it

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: DbContext, Services, Authentication, Authorization, Controllers/Views registered.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: add `AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);` at the top.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend Fix — AppContext Switch for Npgsql | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs | pending | — |
| T-002 | Service Layer Fix — Force UTC Kind | C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Services\AttendanceService.cs, C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Services\LeaveService.cs | pending | T-001 |
