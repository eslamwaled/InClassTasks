using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Session26Api.Common;
using Session26Api.Dtos;
using Session26Api.Entities;
using Session26Api.Repositories;

namespace Session26Api.Services;

public class AuthService(
    IUserRepository userRepository,
    IMapper mapper,
    ITokenService tokenService,
    IPasswordHasher<User> passwordHasher) : IAuthService
{
    public async Task<Result<string>> RegisterAsync(RegisterDto dto)
    {
        var normalizedEmail = NormalizeEmail(dto.Email);

        if (await userRepository.ExistsByEmailAsync(normalizedEmail))
            return Result<string>.Fail(ResultError.Conflict, "Email is already registered.");

        var user = mapper.Map<User>(dto);
        user.Email = normalizedEmail;
        user.UserName = dto.UserName.Trim();
        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return Result<string>.Ok("Registration successful.");
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await userRepository.GetByEmailAsync(NormalizeEmail(dto.Email));
        if (user is null)
            return Result<AuthResponseDto>.Fail(ResultError.Unauthorized, "Invalid credentials.");

        var verification = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verification == PasswordVerificationResult.Failed)
            return Result<AuthResponseDto>.Fail(ResultError.Unauthorized, "Invalid credentials.");

        var (token, expiresAt) = tokenService.GenerateAccessToken(user);

        var response = mapper.Map<AuthResponseDto>(user);
        response.Token = token;
        response.ExpiresAt = expiresAt;

        return Result<AuthResponseDto>.Ok(response);
    }

    private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
}
