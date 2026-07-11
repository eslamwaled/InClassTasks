using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Session27Api.Common;
using Session27Api.DTOs;
using Session27Api.Entities;
using Session27Api.Repositories;

namespace Session27Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.ExistsByEmailAsync(dto.Email))
        {
            return Result<AuthResponseDto>.Conflict("Email is already registered.");
        }

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return Result<AuthResponseDto>.Success(BuildAuthResponse(user));
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user is null)
        {
            return Result<AuthResponseDto>.Unauthorized("Invalid credentials.");
        }

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verification == PasswordVerificationResult.Failed)
        {
            return Result<AuthResponseDto>.Unauthorized("Invalid credentials.");
        }

        return Result<AuthResponseDto>.Success(BuildAuthResponse(user));
    }

    private AuthResponseDto BuildAuthResponse(User user)
    {
        var (token, expiresAt) = _tokenService.GenerateToken(user);
        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;
        response.ExpiresAt = expiresAt;
        return response;
    }
}
