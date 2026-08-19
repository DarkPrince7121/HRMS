using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class DepartmentsController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? searchTerm)
    {
        var departments = await _departmentService.GetAllAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            departments = departments.Where(d => 
                d.Name.ToLower().Contains(searchTerm) || 
                (d.Description != null && d.Description.ToLower().Contains(searchTerm))).ToList();
        }

        return Json(new { data = departments });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] DepartmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _departmentService.CreateAsync(request);
        return Json(new { success = true, data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit([FromBody] DepartmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _departmentService.UpdateAsync(request);
        return Json(new { success });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _departmentService.DeleteAsync(id);
        return Json(new { success });
    }
}