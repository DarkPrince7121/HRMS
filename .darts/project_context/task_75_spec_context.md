# Spec Context — Task 75
**Generated:** 2025-05-14  |  **Framework:** KnockoutJS ASP.NET PostgreSQL  |  **Tasks:** 4

## Gap Analysis Summary
This task involves building the Attendance and Leave management modules for an existing HRMS application. The project is an ASP.NET Core application using PostgreSQL (EF Core) on the backend and KnockoutJS with jQuery/DataTables on the frontend. The required functionality includes entity creation, business logic in services, AJAX-based interactions, and UI components following the established Service/View/ViewModel pattern. New entities for Attendance and Leave need to be integrated into the existing `ApplicationDbContext`, and their respective workflows (marking attendance and requesting/approving leaves) must be implemented.

## Task Plan

### Module: Attendance Management

#### Feature: Attendance Tracking

**T-001: Implement Attendance Tracking — Data, Service, and API**
- **Description:** Create the Attendance entity and database configuration. Implement the `IAttendanceService` to handle marking attendance and retrieving records. Create the `AttendanceController` to expose endpoints for the UI.
- **Files to create:** `HRMS.Models/Entities/Attendance.cs`, `HRMS.Data/Configurations/AttendanceConfiguration.cs`, `HRMS.Services/Interfaces/IAttendanceService.cs`, `HRMS.Services/AttendanceService.cs`, `HRMS.UI/Controllers/AttendanceController.cs`, `HRMS.Models/DTOs/AttendanceRequest.cs`, `HRMS.Models/DTOs/AttendanceResponse.cs`
- **Files to modify:** `HRMS.Data/ApplicationDbContext.cs`, `HRMS.UI/Program.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - Attendance table created in PostgreSQL with `EmployeeId`, `Date`, `Status`, etc.
  - `POST /Attendance/Mark` correctly records attendance for an employee.
  - `GET /Attendance/GetAll` returns attendance records for DataTables.
- **Wiring:**
  - Imports from: `HRMS.Models/Entities/Employee.cs`, `HRMS.Data/ApplicationDbContext.cs`
  - Imported by: `HRMS.UI/wwwroot/js/attendance.js`
  - API routes: `POST /Attendance/Mark`, `GET /Attendance/GetAll`
  - DB tables: `Attendance`, `Employees`
  - Env vars: None

**T-002: Implement Attendance Tracking — UI and ViewModel**
- **Description:** Create the KnockoutJS ViewModel and Razor View for Attendance. Use DataTables to display attendance history and AJAX to submit attendance marks.
- **Files to create:** `HRMS.UI/wwwroot/js/attendance.js`, `HRMS.UI/Views/Attendance/Index.cshtml`
- **Files to modify:** `HRMS.UI/Views/Shared/_Layout.cshtml`
- **Depends on:** T-001
- **Acceptance criteria:**
  - Navigation menu includes "Attendance".
  - Attendance page displays a table of records using DataTables.
  - Clicking "Mark Attendance" sends an AJAX request to the server and refreshes the table.
- **Wiring:**
  - Imports from: `HRMS.UI/Controllers/AttendanceController.cs`
  - Imported by: `HRMS.UI/Views/Attendance/Index.cshtml`
  - API routes: `POST /Attendance/Mark`, `GET /Attendance/GetAll`
  - DB tables: None
  - Env vars: None

### Module: Leave Management

#### Feature: Leave Requests

**T-003: Implement Leave Management — Data, Service, and API**
- **Description:** Create the Leave entity and database configuration. Implement the `ILeaveService` to handle leave requests, status updates (Pending/Approved/Rejected), and validation. Create the `LeaveController` for UI interaction.
- **Files to create:** `HRMS.Models/Entities/Leave.cs`, `HRMS.Data/Configurations/LeaveConfiguration.cs`, `HRMS.Services/Interfaces/ILeaveService.cs`, `HRMS.Services/LeaveService.cs`, `HRMS.UI/Controllers/LeaveController.cs`, `HRMS.Models/DTOs/LeaveRequest.cs`, `HRMS.Models/DTOs/LeaveResponse.cs`
- **Files to modify:** `HRMS.Data/ApplicationDbContext.cs`, `HRMS.UI/Program.cs`
- **Depends on:** None
- **Acceptance criteria:**
  - Leave table created in PostgreSQL with `EmployeeId`, `StartDate`, `EndDate`, `LeaveType`, `Status`.
  - `POST /Leave/Request` creates a new leave record with "Pending" status.
  - `POST /Leave/UpdateStatus` allows changing status to "Approved" or "Rejected".
- **Wiring:**
  - Imports from: `HRMS.Models/Entities/Employee.cs`, `HRMS.Data/ApplicationDbContext.cs`
  - Imported by: `HRMS.UI/wwwroot/js/leaves.js`
  - API routes: `POST /Leave/Request`, `GET /Leave/GetAll`, `POST /Leave/UpdateStatus`
  - DB tables: `Leaves`, `Employees`
  - Env vars: None

**T-004: Implement Leave Management — UI and ViewModel**
- **Description:** Create the KnockoutJS ViewModel and Razor View for Leave requests. Provide a form for submitting leaves and a table for viewing/managing them.
- **Files to create:** `HRMS.UI/wwwroot/js/leaves.js`, `HRMS.UI/Views/Leave/Index.cshtml`
- **Files to modify:** `HRMS.UI/Views/Shared/_Layout.cshtml`
- **Depends on:** T-003
- **Acceptance criteria:**
  - Navigation menu includes "Leaves".
  - Leave page displays a table of requests.
  - Leave request form submits via AJAX and handles validation messages.
  - Admin/Manager can approve/reject leaves from the UI.
- **Wiring:**
  - Imports from: `HRMS.UI/Controllers/LeaveController.cs`
  - Imported by: `HRMS.UI/Views/Leave/Index.cshtml`
  - API routes: `POST /Leave/Request`, `GET /Leave/GetAll`, `POST /Leave/UpdateStatus`
  - DB tables: None
  - Env vars: None

---

## Machine-Readable Task Plan

```json
{
  "modules": [
    {
      "module": "Attendance Management",
      "features": [
        {
          "feature": "Attendance Tracking",
          "tasks": [
            {
              "id": "T-001",
              "name": "Implement Attendance Tracking — Data, Service, and API",
              "description": "Create the Attendance entity and database configuration. Implement the IAttendanceService to handle marking attendance and retrieving records. Create the AttendanceController to expose endpoints for the UI.",
              "files_to_create": [
                "HRMS.Models/Entities/Attendance.cs",
                "HRMS.Data/Configurations/AttendanceConfiguration.cs",
                "HRMS.Services/Interfaces/IAttendanceService.cs",
                "HRMS.Services/AttendanceService.cs",
                "HRMS.UI/Controllers/AttendanceController.cs",
                "HRMS.Models/DTOs/AttendanceRequest.cs",
                "HRMS.Models/DTOs/AttendanceResponse.cs"
              ],
              "files_to_modify": [
                "HRMS.Data/ApplicationDbContext.cs",
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Attendance table created in PostgreSQL with EmployeeId, Date, Status, etc.",
                "POST /Attendance/Mark correctly records attendance for an employee.",
                "GET /Attendance/GetAll returns attendance records for DataTables."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models/Entities/Employee.cs",
                  "HRMS.Data/ApplicationDbContext.cs"
                ],
                "imported_by": [
                  "HRMS.UI/wwwroot/js/attendance.js"
                ],
                "api_routes": [
                  "POST /Attendance/Mark",
                  "GET /Attendance/GetAll"
                ],
                "db_tables": [
                  "Attendance",
                  "Employees"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-002",
              "name": "Implement Attendance Tracking — UI and ViewModel",
              "description": "Create the KnockoutJS ViewModel and Razor View for Attendance. Use DataTables to display attendance history and AJAX to submit attendance marks.",
              "files_to_create": [
                "HRMS.UI/wwwroot/js/attendance.js",
                "HRMS.UI/Views/Attendance/Index.cshtml"
              ],
              "files_to_modify": [
                "HRMS.UI/Views/Shared/_Layout.cshtml"
              ],
              "depends_on": [
                "T-001"
              ],
              "acceptance_criteria": [
                "Navigation menu includes 'Attendance'.",
                "Attendance page displays a table of records using DataTables.",
                "Clicking 'Mark Attendance' sends an AJAX request to the server and refreshes the table."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.UI/Controllers/AttendanceController.cs"
                ],
                "imported_by": [
                  "HRMS.UI/Views/Attendance/Index.cshtml"
                ],
                "api_routes": [
                  "POST /Attendance/Mark",
                  "GET /Attendance/GetAll"
                ],
                "db_tables": [],
                "env_vars": []
              }
            }
          ]
        }
      ]
    },
    {
      "module": "Leave Management",
      "features": [
        {
          "feature": "Leave Requests",
          "tasks": [
            {
              "id": "T-003",
              "name": "Implement Leave Management — Data, Service, and API",
              "description": "Create the Leave entity and database configuration. Implement the ILeaveService to handle leave requests, status updates (Pending/Approved/Rejected), and validation. Create the LeaveController for UI interaction.",
              "files_to_create": [
                "HRMS.Models/Entities/Leave.cs",
                "HRMS.Data/Configurations/LeaveConfiguration.cs",
                "HRMS.Services/Interfaces/ILeaveService.cs",
                "HRMS.Services/LeaveService.cs",
                "HRMS.UI/Controllers/LeaveController.cs",
                "HRMS.Models/DTOs/LeaveRequest.cs",
                "HRMS.Models/DTOs/LeaveResponse.cs"
              ],
              "files_to_modify": [
                "HRMS.Data/ApplicationDbContext.cs",
                "HRMS.UI/Program.cs"
              ],
              "depends_on": [],
              "acceptance_criteria": [
                "Leave table created in PostgreSQL with EmployeeId, StartDate, EndDate, LeaveType, Status.",
                "POST /Leave/Request creates a new leave record with 'Pending' status.",
                "POST /Leave/UpdateStatus allows changing status to 'Approved' or 'Rejected'."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.Models/Entities/Employee.cs",
                  "HRMS.Data/ApplicationDbContext.cs"
                ],
                "imported_by": [
                  "HRMS.UI/wwwroot/js/leaves.js"
                ],
                "api_routes": [
                  "POST /Leave/Request",
                  "GET /Leave/GetAll",
                  "POST /Leave/UpdateStatus"
                ],
                "db_tables": [
                  "Leaves",
                  "Employees"
                ],
                "env_vars": []
              }
            },
            {
              "id": "T-004",
              "name": "Implement Leave Management — UI and ViewModel",
              "description": "Create the KnockoutJS ViewModel and Razor View for Leave requests. Provide a form for submitting leaves and a table for viewing/managing them.",
              "files_to_create": [
                "HRMS.UI/wwwroot/js/leaves.js",
                "HRMS.UI/Views/Leave/Index.cshtml"
              ],
              "files_to_modify": [
                "HRMS.UI/Views/Shared/_Layout.cshtml"
              ],
              "depends_on": [
                "T-003"
              ],
              "acceptance_criteria": [
                "Navigation menu includes 'Leaves'.",
                "Leave page displays a table of requests.",
                "Leave request form submits via AJAX and handles validation messages.",
                "Admin/Manager can approve/reject leaves from the UI."
              ],
              "status": "pending",
              "wiring": {
                "imports_from": [
                  "HRMS.UI/Controllers/LeaveController.cs"
                ],
                "imported_by": [
                  "HRMS.UI/Views/Leave/Index.cshtml"
                ],
                "api_routes": [
                  "POST /Leave/Request",
                  "GET /Leave/GetAll",
                  "POST /Leave/UpdateStatus"
                ],
                "db_tables": [],
                "env_vars": []
              }
            }
          ]
        }
      ]
    }
  ]
}
```
