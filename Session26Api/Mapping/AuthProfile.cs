using AutoMapper;
using Session26Api.Dtos;
using Session26Api.Entities;

namespace Session26Api.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterDto, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(u => u.PasswordHash, opt => opt.Ignore());

        CreateMap<User, AuthResponseDto>()
            .ForMember(r => r.Token, opt => opt.Ignore())
            .ForMember(r => r.ExpiresAt, opt => opt.Ignore());
    }
}
