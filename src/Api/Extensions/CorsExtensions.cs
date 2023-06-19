namespace Api.Extensions;

public static class CorsExtensions
{
    private const string CorsPolicyName = "DefaultCors";

    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName,
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);

        return app;
    }
}
