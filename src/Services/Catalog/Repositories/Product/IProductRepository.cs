using Catalog.Models;
using Catalog.Repositories.Base;

namespace Catalog.Repositories.Product;

public interface IProductRepository : IRepository<Models.Product>
{
    Task<IEnumerable<Models.Product>> GetProductsWithCategoryAsync();
    Task<Models.Product?> GetProductWithCategoryByIdAsync(int id);
    Task<IEnumerable<Models.Product>> GetProductsByCategoryAsync(int categoryId);
}
