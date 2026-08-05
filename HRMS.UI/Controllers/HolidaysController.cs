using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Interfaces;
using HRMS.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.UI.Controllers;

[Authorize]
public class HolidaysController : Controller
{
    private readonly IHolidayService _holidayService;

    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    public IActionResult Index() => View();

    [HttpGet("api/holidays")]
    public async Task<IActionResult> GetAll() => Ok(await _holidayService.GetAllAsync());

    [HttpPost("api/holidays")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] HolidayRequest request) => Ok(await _holidayService.CreateAsync(request));

    [HttpDelete("api/holidays/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _holidayService.DeleteAsync(id);
        return NoContent();
    }
}