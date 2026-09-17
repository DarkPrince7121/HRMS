# Gap Context — As an user, if i tried to navigate to the Applications page, or on clicking the Create New Withdrawal Application, i'm getting error.
On checking the console, it shows like the the table 'WithdrawalApplications' does not exists and on checking the migration history,
there is only two migrations applied "20260821072039_AddUserIdToEmployee"
"20260902073628_AddApplicationStatusTable" and there is no other migrations applied for AddEmployeeSalaryBalanceTable and the AddWithdrawalApplicationTable. on further checking the both in the code base there is no designer file got created for the both. so analyze the root cause and fix it. you can use .net ef core migration tools for this and make sure there is no other issue in this.
so, as an user if i navigate to the Applications page, i should see the list of applications created , and on clicking the create new withdrawal application should navigate me to the application creation wizard. check this and fix this.
**Date:** 2026-09-02  |  **Task ID:** 118  |  **Type:** Brownfield

## Project Overview

### Tech Stack
- **Backend:** ASP.NET Core 6.0
- **Database:** PostgreSQL with Entity Framework Core
- **Frontend:** KnockoutJS, Razor Views, Bootstrap

### Existing Modules & Features
- **Data Context & Configurations** (`HRMS-ApplicationProcess/HRMS.Data/`): Contains `ApplicationDbContext` and configuration mappings for entities such as `WithdrawalApplicationConfiguration` and `EmployeeSalaryBalanceConfiguration`.
- **Database Migrations** (`HRMS-ApplicationProcess/HRMS.Data/Migrations/`): Tracks schema changes, currently containing incomplete migration files for salary balances and withdrawal applications.
- **Applications Controller** (`HRMS-ApplicationProcess/HRMS.UI/Controllers/ApplicationsController.cs`): Defines REST APIs and MVC routes for fetching and initiating withdrawal applications.
- **Applications View** (`HRMS-ApplicationProcess/HRMS.UI/Views/Applications/Index.cshtml`): Lists the existing applications and hosts the navigation to initiate new application wizards.

### Prior Context
No prior analysis found for this project.

## Requirements Analysis

### Extracted Requirements
1. **Analyze and resolve migration issues**: Determine why `AddEmployeeSalaryBalanceTable` and `AddWithdrawalApplicationTable` are missing `.Designer.cs` metadata files and have not been executed on the PostgreSQL database.
2. **Apply pending database migrations**: Regenerate, fix or complete the missing designer files using Entity Framework Core migration tools so the schema contains the `EmployeeSalaryBalances` and `WithdrawalApplications` tables.
3. **Establish seamless page navigation**: Ensure a user navigating to `/Applications` gets the complete lists of applications without database errors, and clicking the "Create New Withdrawal Application" button routes or opens the wizard.

### Requirements Mapping
| Requirement | Status | Location in Codebase | Notes |
|---|---|---|---|
| Analyze and resolve migration issues | Needs Modification | `HRMS-ApplicationProcess/HRMS.Data/Migrations/` | The files `20260902073901_AddEmployeeSalaryBalanceTable.cs` and `20260902074721_AddWithdrawalApplicationTable.cs` exist but lack `.Designer.cs` snapshots. |
| Apply pending database migrations | Needs Modification | `HRMS-ApplicationProcess/HRMS.Data/` | Execute .NET EF Core migration tools or manually generate/recreate the designer metadata matching the state in `ApplicationDbContextModelSnapshot.cs` to enable successful database migration. |
| Verify list and wizard navigation | Already Exists | `HRMS-ApplicationProcess/HRMS.UI/Controllers/ApplicationsController.cs`, `HRMS-ApplicationProcess/HRMS.UI/Views/Applications/Index.cshtml` | Once the database tables exist, the page routes and views will function cleanly. |

## Tech Stack & Implementation

### Database Schema Alignment & Migration Generation — Needs Modification
- **Approach:** Regenerate the missing migration files or design files using the Entity Framework CLI tools (e.g., `dotnet ef database update` or `dotnet ef migrations add` after clean-up) to align with `ApplicationDbContextModelSnapshot.cs`. Ensure that the correct metadata files are created and PostgreSQL tables are fully instantiated.
- **Existing files to modify:** 
  - `HRMS-ApplicationProcess/HRMS.Data/Migrations/20260902073901_AddEmployeeSalaryBalanceTable.cs`
  - `HRMS-ApplicationProcess/HRMS.Data/Migrations/20260902074721_AddWithdrawalApplicationTable.cs`
- **New dependencies:** None

## Summary
The project is a mature Brownfield ASP.NET Core web application utilizing KnockoutJS and PostgreSQL. The system supports full application processing and status tracking for withdrawal applications, but currently encounters database runtime exceptions because the required `WithdrawalApplications` and `EmployeeSalaryBalances` tables do not exist in PostgreSQL.

This task is focused on troubleshooting database schema inconsistencies. Specifically, two migrations was written/generated partially without their respective `.Designer.cs` metadata files, which prevented EF Core from executing them. Resolving this issue involves using .NET EF Core migration tools to fix the migration history and metadata, then applying the migrations to create the required tables. Once the database is aligned, the existing applications view and creation wizard will be fully operational.
