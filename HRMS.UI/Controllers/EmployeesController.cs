using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? searchTerm, int? departmentId)
    {
        var employees = await _employeeService.GetAllAsync();
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            employees = employees.Where(e => 
                e.FirstName.ToLower().Contains(searchTerm) || 
                e.LastName.ToLower().Contains(searchTerm) || 
                e.Email.ToLower().Contains(searchTerm)).ToList();
        }

        if (departmentId.HasValue && departmentId.Value > 0)
        {
            employees = employees.Where(e => e.DepartmentId == departmentId.Value).ToList();
        }

        return Json(new { data = employees });
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] EmployeeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _employeeService.CreateAsync(request);
        return Json(new { success = true, data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit([FromBody] EmployeeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _employeeService.UpdateAsync(request);
        return Json(new { success });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _employeeService.DeleteAsync(id);
        if (!success) return NotFound();
        return Json(new { success });
    }
}