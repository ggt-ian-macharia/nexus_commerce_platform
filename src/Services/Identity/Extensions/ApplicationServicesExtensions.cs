using Identity.Services;

namespace Identity.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TokenService>();
        
        // Add AutoMapper
        services.AddAutoMapper(typeof(Program).Assembly);
        
        return services;
    }
}
