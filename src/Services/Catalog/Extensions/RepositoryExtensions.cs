using Catalog.Repositories.Product;
using UoW = Catalog.Data.UnitOfWork;

namespace Catalog.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<UoW.IUnitOfWork, UoW.UnitOfWork>();
        
        return services;
    }
}
