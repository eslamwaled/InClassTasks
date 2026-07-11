using Session26Api.Common;
using Session26Api.Dtos;

namespace Session26Api.Services;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(RegisterDto dto);
    Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
}
