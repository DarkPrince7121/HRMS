using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Interfaces;

namespace HRMS.UI.Controllers;

public class AuditController : Controller
{
    private readonly IAuditService _auditService;

    public AuditController(IAuditService auditService)
    {
        _auditService = auditService;
    }

    public IActionResult Index() => View();

    [HttpGet("api/audit")]
    public async Task<IActionResult> GetAll() => Ok(await _auditService.GetAllAsync());
}