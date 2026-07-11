using Microsoft.AspNetCore.Mvc;
using Session27Api.DTOs;
using Session27Api.Services;

namespace Session27Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(result.StatusCode, new { message = result.Error });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(result.StatusCode, new { message = result.Error });
    }
}
