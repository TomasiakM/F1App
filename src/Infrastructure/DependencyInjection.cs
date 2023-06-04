using Application.Interfaces;
using Domain.Aggregates.Roles.ValueObjects;
using Domain.Aggregates.Users;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddDbContext<AppDbContext>();

        services.AddAppAuthorization(configuration);

        services.AddScoped<IDateProvider, DateService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static async Task<IApplicationBuilder> AddInfrastructure(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        using var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

        if (dbContext is null)
        {
            throw new NullReferenceException(nameof(AppDbContext));
        }

        await dbContext.Database.MigrateAsync();

        var adminModel = User.Create("Admin", new HashService().Hash("Password"), "admin@email.com");
        adminModel.AddRole(RoleId.AdminRoleId);

        var admin = await dbContext.Set<User>()
            .FirstOrDefaultAsync(e => e.Username == adminModel.Username);

        if (admin is null)
        {
            dbContext.Add(adminModel);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
