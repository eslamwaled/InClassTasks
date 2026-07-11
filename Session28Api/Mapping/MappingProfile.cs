using AutoMapper;
using Session28Api.DTOs;
using Session28Api.Entities;

namespace Session28Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDto, User>()
            .ForMember(user => user.Id, options => options.Ignore())
            .ForMember(user => user.PasswordHash, options => options.Ignore());

        CreateMap<User, AuthResponseDto>()
            .ForMember(response => response.Token, options => options.Ignore())
            .ForMember(response => response.ExpiresAt, options => options.Ignore());

        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductDto, Product>()
            .ForMember(product => product.Id, options => options.Ignore());
    }
}
