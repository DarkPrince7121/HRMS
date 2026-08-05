using HRMS.Data;
using HRMS.Models.DTOs;
using HRMS.Models.Entities;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services;

public class SettingService : ISettingService
{
    private readonly ApplicationDbContext _context;

    public SettingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SystemSetting>> GetAllSettingsAsync()
    {
        return await _context.SystemSettings.ToListAsync();
    }

    public async Task<string> GetSettingValueAsync(string key, string defaultValue = "")
    {
        var setting = await _context.SystemSettings.FindAsync(key);
        return setting?.Value ?? defaultValue;
    }

    public async Task UpdateSettingAsync(SettingRequest request)
    {
        var setting = await _context.SystemSettings.FindAsync(request.Key);
        if (setting != null)
        {
            setting.Value = request.Value;
            await _context.SaveChangesAsync();
        }
    }
}