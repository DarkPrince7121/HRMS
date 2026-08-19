# Planner-Coder Todo — 99
**Requirement:** As an user, If i try to open/run the project in any system, i shouldn't get any error, but i could sense that the postgres instance should be created in docker container before running this app, so, can you create a docker yaml file with the connection string provided in it, so that if any user wants to try using the application must execute the docker command to create the postgres instance and then they can start the app, and the app will automatically create database/initial migrations in it

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: AppContext switch, builder.Services (DbContext, Scoped services, Auth, Authz), app.Build(), Database initialization scope, app.Use middleware (ExceptionHandler, Hsts, HttpsRedirection, StaticFiles, Routing, Authentication, Authorization), MapControllerRoute, app.Run()
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: builder.Services (DbContext, Scoped services, Quartz), app.Build(), Database migration scope, app.MapGet, app.Run()

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\docker-compose.yml: Update to ensure it matches the connection string in appsettings.json.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.UI\Program.cs: Ensure database migration logic is robust.
- C:\DARTS-development-environment\sandbox\rmanoj\HRMS\HRMS.Web\Program.cs: Ensure database migration logic is robust.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Infrastructure and Entry Point Wiring | docker-compose.yml, HRMS.UI/Program.cs, HRMS.Web/Program.cs | pending | — |
