using Session27Api.Common;
using Session27Api.DTOs;

namespace Session27Api.Services;

public interface IAdminService
{
    Task<Result<IReadOnlyList<UserDto>>> GetUsersAsync();
}
