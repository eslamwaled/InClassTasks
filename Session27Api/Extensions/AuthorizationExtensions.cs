namespace Session27Api.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanManageProducts", policy => policy.RequireRole("Admin"));
        });

        return services;
    }
}
