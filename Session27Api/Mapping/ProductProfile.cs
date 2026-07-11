using AutoMapper;
using Session27Api.DTOs;

namespace Session27Api.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, ProductDto>();
    }
}
