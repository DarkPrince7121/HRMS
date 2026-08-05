using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;

namespace HRMS.Services;

public class HolidayService : IHolidayService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditService _auditService;

    public HolidayService(ApplicationDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<IEnumerable<HolidayResponse>> GetAllAsync()
    {
        return await _context.Holidays
            .Select(x => new HolidayResponse
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date,
                IsRecurring = x.IsRecurring
            }).ToListAsync();
    }

    public async Task<HolidayResponse> CreateAsync(HolidayRequest request)
    {
        var holiday = new Holiday
        {
            Name = request.Name,
            Date = request.Date,
            IsRecurring = request.IsRecurring
        };

        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();

        await _auditService.LogActionAsync("Holiday", holiday.Id.ToString(), "Create", $"Name: {holiday.Name}, Date: {holiday.Date:yyyy-MM-dd}", "System");

        return new HolidayResponse
        {
            Id = holiday.Id,
            Name = holiday.Name,
            Date = holiday.Date,
            IsRecurring = holiday.IsRecurring
        };
    }

    public async Task DeleteAsync(int id)
    {
        var holiday = await _context.Holidays.FindAsync(id);
        if (holiday != null)
        {
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            await _auditService.LogActionAsync("Holiday", id.ToString(), "Delete", "", "System");
        }
    }
}