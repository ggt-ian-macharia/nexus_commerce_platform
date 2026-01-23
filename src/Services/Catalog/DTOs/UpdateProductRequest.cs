namespace Catalog.DTOs;

public record UpdateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public int Stock { get; init; }
    public int CategoryId { get; init; }
}
