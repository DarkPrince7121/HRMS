using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize(Policy = "AdminOnly")]
public class SettingsController : Controller
{
    private readonly ISettingService _settingService;

    public SettingsController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public IActionResult Index() => View();

    [HttpGet("api/settings")]
    public async Task<IActionResult> GetSettings()
    {
        var settings = await _settingService.GetAllSettingsAsync();
        return Ok(settings);
    }

    [HttpPost("api/settings")]
    public async Task<IActionResult> UpdateSetting([FromBody] SettingRequest request)
    {
        await _settingService.UpdateSettingAsync(request);
        return Ok();
    }
}