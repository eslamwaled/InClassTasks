using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Session27Api.DTOs;
using Session27Api.Services;

namespace Session27Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _productService.GetAll();
        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateProductDto dto)
    {
        var result = _productService.Add(dto);
        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(result.StatusCode, new { message = result.Error });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "CanManageProducts")]
    public IActionResult Delete(int id)
    {
        var result = _productService.Delete(id);
        return result.IsSuccess
            ? Ok()
            : StatusCode(result.StatusCode, new { message = result.Error });
    }
}
