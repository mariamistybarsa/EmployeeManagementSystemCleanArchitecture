using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Application.DTO.Request.Auth;
using EmployeeManagement.Application.Services.Interfaces.Auth;

namespace EmployeeManagement.WebAPI.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _service.LoginAsync(request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _service.RegisterAsync(request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }
}