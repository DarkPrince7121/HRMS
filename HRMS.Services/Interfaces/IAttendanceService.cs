using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceResponse>> GetAllAsync();
    Task<AttendanceResponse> MarkAsync(AttendanceRequest request);
}