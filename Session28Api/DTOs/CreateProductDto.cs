using System.ComponentModel.DataAnnotations;

namespace Session28Api.DTOs;

public class CreateProductDto
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}
