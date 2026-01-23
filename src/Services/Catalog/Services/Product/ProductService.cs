using AutoMapper;
using Catalog.Data.UnitOfWork;
using Catalog.DTOs;
using EventBus.Events;
using MassTransit;

namespace Catalog.Services.Product;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetProductWithCategoryByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetProductsWithCategoryAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var product = _mapper.Map<Models.Product>(request);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            // Reload with category for response
            var createdProduct = await _unitOfWork.Products.GetProductWithCategoryByIdAsync(product.Id);

            // Publish event
            var productCreatedEvent = new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt
            };
            await _publishEndpoint.Publish(productCreatedEvent);

            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<ProductDto>(createdProduct);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }

        _mapper.Map(request, product);
        product.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }

        // Soft delete
        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}
