using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rotas.DataAccess.FileDataAccess.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupFileDataAccess(this IServiceCollection services, IConfiguration? configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var path = configuration["PersistenceFileName"];
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(configuration), "PersistenceFileName not found in configuration");

        services.AddSingleton<IRepositoryCrud<Viagem>>(provider => new RepositoryCrudViagem(path));
        return services;
    }
}

