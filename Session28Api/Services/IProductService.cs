using Session28Api.Common;
using Session28Api.DTOs;

namespace Session28Api.Services;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetAllAsync();

    Task<Result<ProductDto>> GetByIdAsync(int id);

    Task<Result<ProductDto>> CreateAsync(CreateProductDto dto);

    Task<Result<bool>> DeleteAsync(int id);
}
