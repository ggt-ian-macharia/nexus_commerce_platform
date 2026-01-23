using Catalog.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection") 
                ?? "Host=localhost;Database=nexus_catalog;Username=postgres;Password=postgres"));

        return services;
    }
}
