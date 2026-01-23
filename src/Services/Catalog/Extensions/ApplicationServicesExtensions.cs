namespace Catalog.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add AutoMapper
        services.AddAutoMapper(typeof(Program).Assembly);
        
        // Add Services
        services.AddScoped<Services.Product.IProductService, Services.Product.ProductService>();
        
        return services;
    }
}
