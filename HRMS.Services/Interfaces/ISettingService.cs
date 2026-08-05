using HRMS.Models.DTOs;
using HRMS.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces;

public interface ISettingService
{
    Task<IEnumerable<SystemSetting>> GetAllSettingsAsync();
    Task<string> GetSettingValueAsync(string key, string defaultValue = "");
    Task UpdateSettingAsync(SettingRequest request);
}