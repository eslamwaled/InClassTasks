using Microsoft.AspNetCore.Identity;
using Session28Api.Entities;
using Session28Api.Mapping;
using Session28Api.Repositories;
using Session28Api.Services;

namespace Session28Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddDatabase(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddSwaggerDocumentation();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
