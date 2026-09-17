using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class DesignationsController : Controller
{
    private readonly IDesignationService _designationService;

    public DesignationsController(IDesignationService designationService)
    {
        _designationService = designationService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? searchTerm)
    {
        var designations = await _designationService.GetAllAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            designations = designations.Where(d => 
                d.Name.ToLower().Contains(searchTerm) || 
                (d.Description != null && d.Description.ToLower().Contains(searchTerm))).ToList();
        }

        return Json(new { data = designations });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] DesignationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _designationService.CreateAsync(request);
        return Json(new { success = true, data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit([FromBody] DesignationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _designationService.UpdateAsync(request);
        return Json(new { success });
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _designationService.DeleteAsync(id);
        return Json(new { success = true });
    }
}