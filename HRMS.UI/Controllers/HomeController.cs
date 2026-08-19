using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRMS.Services.Interfaces;
using HRMS.Models.DTOs;

namespace HRMS.UI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly ILeaveService _leaveService;

    public HomeController(
        IEmployeeService employeeService, 
        IDepartmentService departmentService, 
        ILeaveService leaveService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _leaveService = leaveService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("api/dashboard/stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var employees = await _employeeService.GetAllAsync();
        var departments = await _departmentService.GetAllAsync();
        var leaves = await _leaveService.GetAllAsync();

        var stats = new DashboardStatsResponse
        {
            EmployeeCount = employees.Count(),
            DepartmentCount = departments.Count(),
            ActiveLeaveCount = leaves.Count(l => l.Status == "Approved" && l.StartDate <= DateTime.Today && l.EndDate >= DateTime.Today)
        };

        return Json(stats);
    }
}