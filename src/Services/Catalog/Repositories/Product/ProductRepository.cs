using Catalog.Data;
using Catalog.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories.Product;

public class ProductRepository : Repository<Models.Product>, IProductRepository
{
    public ProductRepository(CatalogDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Models.Product>> GetProductsWithCategoryAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Models.Product?> GetProductWithCategoryByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Models.Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}
