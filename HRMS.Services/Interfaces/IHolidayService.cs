using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IHolidayService
{
    Task<IEnumerable<HolidayResponse>> GetAllAsync();
    Task<HolidayResponse> CreateAsync(HolidayRequest request);
    Task DeleteAsync(int id);
}