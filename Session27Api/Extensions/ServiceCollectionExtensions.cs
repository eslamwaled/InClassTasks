using Microsoft.AspNetCore.Identity;
using Session27Api.Entities;
using Session27Api.Options;
using Session27Api.Repositories;
using Session27Api.Services;

namespace Session27Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddControllers();
        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddDatabase(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddAuthorizationPolicies();
        services.AddCorsPolicies(configuration);
        services.AddSwaggerWithJwt();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddSingleton<IProductService, ProductService>();

        return services;
    }
}
