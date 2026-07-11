using Session28Api.Entities;

namespace Session28Api.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) CreateToken(User user);
}
