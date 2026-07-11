using AutoMapper;
using Session27Api.Common;
using Session27Api.DTOs;
using Session27Api.Repositories;

namespace Session27Api.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AdminService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<UserDto>>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var dtos = _mapper.Map<IReadOnlyList<UserDto>>(users);
        return Result<IReadOnlyList<UserDto>>.Success(dtos);
    }
}
