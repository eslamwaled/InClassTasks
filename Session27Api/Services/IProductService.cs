using Session27Api.Common;
using Session27Api.DTOs;

namespace Session27Api.Services;

public interface IProductService
{
    Result<IReadOnlyList<ProductDto>> GetAll();
    Result<ProductDto> Add(CreateProductDto dto);
    Result<bool> Delete(int id);
}
