using AutoMapper;
using Session28Api.Common;
using Session28Api.DTOs;
using Session28Api.Entities;
using Session28Api.Repositories;

namespace Session28Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return Result<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products));
    }

    public async Task<Result<ProductDto>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return Result<ProductDto>.Failure(ResultError.NotFound, $"Product with id {id} was not found.");
        }

        return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return Result<bool>.Failure(ResultError.NotFound, $"Product with id {id} was not found.");
        }

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}
