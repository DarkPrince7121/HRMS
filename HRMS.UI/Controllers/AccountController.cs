using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.DTOs;
using HRMS.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using HRMS.Services.Interfaces;

namespace HRMS.UI.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IEmployeeService _employeeService;

    public AccountController(ApplicationDbContext context, IEmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
        _passwordHasher = new PasswordHasher<User>();
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");
            
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Check if employee exists
            var employee = await _employeeService.GetByEmailAsync(user.Email);
            bool needsEmployeeRegistration = employee == null;

            return Ok(new { success = true, needsEmployeeRegistration });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Username and password are required" });
            }

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Employee") 
                            ?? await _context.Roles.FirstOrDefaultAsync();

            if (defaultRole == null)
            {
                defaultRole = new Role { Name = "Employee" };
                _context.Roles.Add(defaultRole);
                await _context.SaveChangesAsync();
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email ?? "",
                RoleId = defaultRole.Id,
                PasswordHash = ""
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult RegisterEmployee()
    {
        if (User.Identity?.IsAuthenticated != true)
            return RedirectToAction("Login");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRequest request)
    {
        try
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
                return Unauthorized();

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            
            var userId = int.Parse(userIdClaim);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName))
            {
                return BadRequest(new { message = "First and Last Name are required" });
            }

            // Set default IT department if not provided
            if (request.DepartmentId <= 0)
            {
                var itDept = await _context.Departments.FirstOrDefaultAsync(d => d.Name == "IT");
                if (itDept == null)
                {
                    itDept = new Department { Name = "IT", Code = "IT", Description = "Information Technology" };
                    _context.Departments.Add(itDept);
                    await _context.SaveChangesAsync();
                }
                request.DepartmentId = itDept.Id;
            }

            request.Email = userEmail ?? "";
            request.UserId = userId;

            await _employeeService.CreateAsync(request);

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}