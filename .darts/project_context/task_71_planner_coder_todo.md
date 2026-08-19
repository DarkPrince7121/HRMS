# Planner-Coder Todo — 71
**Requirement:** Create the base .NET 6 solution with projects: HRMS.Data (Persistence), HRMS.Models (DTOs/Entities), HRMS.Services (Logic), HRMS.Web (Background Jobs), and HRMS.UI (Web Application). Initialize folder structures for each as specified in the architecture.

Acceptance Criteria:
- Solution file (.sln) contains five projects: HRMS.Data, HRMS.Models, HRMS.Services, HRMS.Web, and HRMS.UI.
- All projects target .NET 6.0.
- Folder structures for each project match the mandatory segregation by domain.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- None (New project initialization)

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- HRMS.Data.csproj: reference HRMS.Models
- HRMS.Services.csproj: reference HRMS.Models, HRMS.Data
- HRMS.Web.csproj: reference HRMS.Services, HRMS.Data, HRMS.Models
- HRMS.UI.csproj: reference HRMS.Services, HRMS.Data, HRMS.Models

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Create Solution and Projects | HRMS.sln, HRMS.Data/HRMS.Data.csproj, HRMS.Models/HRMS.Models.csproj, HRMS.Services/HRMS.Services.csproj, HRMS.Web/HRMS.Web.csproj, HRMS.UI/HRMS.UI.csproj | pending | — |
| T-002 | Initialize Folder Structures | HRMS.Data/Repositories/, HRMS.Data/Configurations/, HRMS.Models/Entities/, HRMS.Models/DTOs/, HRMS.Services/Interfaces/, HRMS.Services/Implementations/, HRMS.Web/Jobs/, HRMS.UI/Controllers/, HRMS.UI/Views/, HRMS.UI/wwwroot/ | pending | T-001 |
| T-003 | Setup Entry Points and Basic Wiring | HRMS.UI/Program.cs, HRMS.Web/Program.cs, HRMS.Data/ApplicationDbContext.cs | pending | T-002 |