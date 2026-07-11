using Microsoft.AspNetCore.Identity;
using Session26Api.Entities;
using Session26Api.Mapping;
using Session26Api.Repositories;
using Session26Api.Services;

namespace Session26Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddApiDocumentation();
        services.AddApplicationDependencies();
        return services;
    }

    private static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddAutoMapper(typeof(AuthProfile));
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
