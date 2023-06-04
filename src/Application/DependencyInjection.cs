﻿using Application.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapper();
        services.AddMediatr();
        services.AddFluentValidation();

        return services;
    }
}
