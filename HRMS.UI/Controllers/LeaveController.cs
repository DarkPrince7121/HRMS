using System.Threading.Tasks;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class LeaveController : Controller
{
    private readonly ILeaveService _leaveService;

    public LeaveController(ILeaveService leaveService)
    {
        _leaveService = leaveService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var records = await _leaveService.GetAllAsync();
        return Json(new { data = records });
    }

    [HttpGet]
    public async Task<IActionResult> RequestLeave()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RequestLeave([FromBody] LeaveRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _leaveService.RequestAsync(request);
        return Json(new { success = true, data = result });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var success = await _leaveService.UpdateStatusAsync(id, status);
        return Json(new { success });
    }
}