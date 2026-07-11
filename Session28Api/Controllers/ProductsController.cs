using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Session28Api.DTOs;
using Session28Api.Services;

namespace Session28Api.Controllers;

/// <summary>
/// Manages the product catalog.
/// </summary>
[Route("api/products")]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="productService">The product service.</param>
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Returns all products. No authentication required.
    /// </summary>
    /// <returns>The full list of products.</returns>
    /// <response code="200">The product list was returned.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductDto>>> GetAll() =>
        ToActionResult(await _productService.GetAllAsync());

    /// <summary>
    /// Returns a single product by its identifier. No authentication required.
    /// </summary>
    /// <param name="id">The identifier of the product to fetch.</param>
    /// <returns>The requested product.</returns>
    /// <response code="200">The product was found and returned.</response>
    /// <response code="404">No product exists with the given identifier.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id) =>
        ToActionResult(await _productService.GetByIdAsync(id));

    /// <summary>
    /// Creates a new product. Requires a valid JWT bearer token.
    /// </summary>
    /// <param name="dto">The product to create: name and price.</param>
    /// <returns>The created product including its generated identifier.</returns>
    /// <response code="200">The product was created and returned.</response>
    /// <response code="400">The request body failed validation.</response>
    /// <response code="401">No valid JWT bearer token was provided.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto) =>
        ToActionResult(await _productService.CreateAsync(dto));

    /// <summary>
    /// Deletes an existing product. Requires a valid JWT bearer token.
    /// </summary>
    /// <param name="id">The identifier of the product to delete.</param>
    /// <returns>A confirmation that the product was deleted.</returns>
    /// <response code="200">The product was deleted.</response>
    /// <response code="401">No valid JWT bearer token was provided.</response>
    /// <response code="404">No product exists with the given identifier.</response>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(int id) =>
        ToActionResult(await _productService.DeleteAsync(id));
}
