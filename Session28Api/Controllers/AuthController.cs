using Microsoft.AspNetCore.Mvc;
using Session28Api.DTOs;
using Session28Api.Services;

namespace Session28Api.Controllers;

/// <summary>
/// Handles user registration and login.
/// </summary>
[Route("api/auth")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="dto">The registration details: email, user name and password.</param>
    /// <returns>The created account details together with a signed JWT access token.</returns>
    /// <response code="200">The account was created and a token was issued.</response>
    /// <response code="400">The request body failed validation.</response>
    /// <response code="409">A user with the same email already exists.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto) =>
        ToActionResult(await _authService.RegisterAsync(dto));

    /// <summary>
    /// Authenticates an existing user and issues a JWT access token.
    /// </summary>
    /// <param name="dto">The login credentials: email and password.</param>
    /// <returns>The authenticated account details together with a signed JWT access token.</returns>
    /// <response code="200">The credentials were valid and a token was issued.</response>
    /// <response code="400">The request body failed validation.</response>
    /// <response code="401">The email or password is incorrect.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto) =>
        ToActionResult(await _authService.LoginAsync(dto));
}
