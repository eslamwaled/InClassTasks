using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Session26Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private static readonly string[] Products = ["Keyboard", "Mouse", "Monitor", "Headset"];

    [HttpGet]
    public IActionResult GetAll() => Ok(Products);

    [Authorize]
    [HttpPost]
    public IActionResult Create([FromBody] string name)
        => Created($"api/products/{Products.Length}", new { name, createdBy = User.Identity?.Name });
}
