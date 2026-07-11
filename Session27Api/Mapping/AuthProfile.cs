using AutoMapper;
using Session27Api.DTOs;
using Session27Api.Entities;

namespace Session27Api.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, AuthResponseDto>();
        CreateMap<User, UserDto>();
    }
}
