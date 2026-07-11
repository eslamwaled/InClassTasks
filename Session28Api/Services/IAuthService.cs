using Session28Api.Common;
using Session28Api.DTOs;

namespace Session28Api.Services;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);

    Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
}
