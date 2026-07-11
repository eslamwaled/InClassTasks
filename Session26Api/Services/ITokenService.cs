using Session26Api.Entities;

namespace Session26Api.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);
}
