using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rotas.Domain.Services;

namespace Rotas.Domain.DependencyInjection;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection SetupDomain(this IServiceCollection services)
    {
        services.AddScoped<ViagemValidationService>();
        return services;
    }
}
