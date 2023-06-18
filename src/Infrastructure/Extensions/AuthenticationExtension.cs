using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions;
internal static class AuthenticationExtension
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        var cookieSettings = new CookieSettings();
        configuration.GetSection(nameof(CookieSettings)).Bind(cookieSettings);

        services.AddOptions<JwtSettings>()
            .BindConfiguration(nameof(JwtSettings));

        services.AddAuthentication(e =>
        {
            e.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, e =>
            {
                e.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuer = true,

                    ValidAudience = jwtSettings.Issuer,
                    ValidateAudience = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true,
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = cookieSettings.Name;
                options.ExpireTimeSpan = TimeSpan.FromDays(cookieSettings.ExpiryDays);

                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;


                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    }
                };

                options.SlidingExpiration = true;
            });

        services.AddAuthorization();

        return services;
    }
}
