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
        var responseList = new List<PayrollResponse>();

        foreach (var p in payrolls)
        {
            var breakdown = await _payrollService.GetPayrollBreakdownAsync(p.EmployeeId, p.PayDate, p.GrossPay);
            responseList.Add(new PayrollResponse
            { 
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                EmployeeName = p.Employee.FirstName + " " + p.Employee.LastName,
                PayDate = p.PayDate,
                GrossPay = p.GrossPay,
                Deductions = p.Deductions,
                NetPay = p.NetPay,
                BasicPay = breakdown.BasicPay,
                Allowances = breakdown.Allowances,
                LeaveDeductions = breakdown.LeaveDeductions,
                AbsentDeductions = breakdown.AbsentDeductions,
                Status = p.Status
            });
        }
        return Json(new { data = responseList });
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