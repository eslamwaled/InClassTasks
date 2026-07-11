namespace Session27Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            options.AddPolicy("Production", policy =>
                policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
        });

        return services;
    }
}
