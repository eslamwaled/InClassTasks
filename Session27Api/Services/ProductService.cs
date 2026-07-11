using AutoMapper;
using Session27Api.Common;
using Session27Api.DTOs;

namespace Session27Api.Services;

public class ProductService : IProductService
{
    private static readonly object Sync = new();
    private static int _nextId = 4;
    private static readonly List<ProductDto> Products =
    [
        new ProductDto { Id = 1, Name = "Keyboard", Price = 49.99m },
        new ProductDto { Id = 2, Name = "Mouse", Price = 19.99m },
        new ProductDto { Id = 3, Name = "Monitor", Price = 199.99m }
    ];

    private readonly IMapper _mapper;

    public ProductService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Result<IReadOnlyList<ProductDto>> GetAll()
    {
        lock (Sync)
        {
            return Result<IReadOnlyList<ProductDto>>.Success(Products.ToList());
        }
    }

    public Result<ProductDto> Add(CreateProductDto dto)
    {
        var product = _mapper.Map<ProductDto>(dto);
        lock (Sync)
        {
            product.Id = _nextId++;
            Products.Add(product);
        }
        return Result<ProductDto>.Success(product);
    }

    public Result<bool> Delete(int id)
    {
        lock (Sync)
        {
            var removed = Products.RemoveAll(p => p.Id == id);
            return removed > 0
                ? Result<bool>.Success(true)
                : Result<bool>.NotFound("Product not found.");
        }
    }
}
