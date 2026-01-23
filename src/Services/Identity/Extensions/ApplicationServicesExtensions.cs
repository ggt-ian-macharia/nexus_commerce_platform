using Identity.Services;

namespace Identity.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Token Service
        services.AddScoped<Services.Token.TokenService>();
        
        // Repositories
        services.AddScoped<Repositories.User.IUserRepository, Repositories.User.UserRepository>();
        
        // Services
        services.AddScoped<Services.Auth.IAuthService, Services.Auth.AuthService>();
        
        // Add AutoMapper
        services.AddAutoMapper(typeof(Program).Assembly);
        
        return services;
    }
}
