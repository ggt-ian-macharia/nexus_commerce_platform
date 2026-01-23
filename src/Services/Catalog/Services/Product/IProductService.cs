using Catalog.DTOs;

namespace Catalog.Services.Product;

public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(int id, UpdateProductRequest request);
    Task DeleteProductAsync(int id);
}
