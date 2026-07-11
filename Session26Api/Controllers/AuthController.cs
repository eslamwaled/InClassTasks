using Microsoft.AspNetCore.Mvc;
using Session26Api.Common;
using Session26Api.Dtos;
using Session26Api.Services;

namespace Session26Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await authService.RegisterAsync(dto);
        return result.Success ? Ok(new { message = result.Data }) : ToErrorResult(result.Error, result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await authService.LoginAsync(dto);
        return result.Success ? Ok(result.Data) : ToErrorResult(result.Error, result.ErrorMessage);
    }

    private IActionResult ToErrorResult(ResultError error, string? message) => error switch
    {
        ResultError.Conflict => Conflict(new { message }),
        ResultError.Unauthorized => Unauthorized(new { message }),
        _ => BadRequest(new { message })
    };
}
