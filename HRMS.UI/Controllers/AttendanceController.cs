using System.Threading.Tasks;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var records = await _attendanceService.GetAllAsync();
        return Json(new { data = records });
    }

    [HttpGet]
    public async Task<IActionResult> Mark()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> RequestLeave()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Mark([FromBody] AttendanceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _attendanceService.MarkAsync(request);
        return Json(new { success = true, data = result });
    }
}