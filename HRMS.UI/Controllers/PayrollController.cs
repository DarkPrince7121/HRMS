using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.UI.Controllers;

[Authorize]
public class PayrollController : Controller
{
    private readonly IPayrollService _payrollService;

    public PayrollController(IPayrollService payrollService)
    {
        _payrollService = payrollService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payrolls = await _payrollService.GetAllAsync();
        var response = payrolls.Select(p => new PayrollResponse
        {
            Id = p.Id,
            EmployeeId = p.EmployeeId,
            EmployeeName = p.Employee.FirstName + " " + p.Employee.LastName,
            PayDate = p.PayDate,
            GrossPay = p.GrossPay,
            Deductions = p.Deductions,
            NetPay = p.NetPay,
            Status = p.Status
        });
        return Json(new { data = response });
    }

    [HttpGet]
    public async Task<IActionResult> Generate()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Generate([FromBody] PayrollRequest request)
    { 
        await _payrollService.GeneratePayrollForMonthAsync(request.Month);
        return Json(new { success = true });
    }
}