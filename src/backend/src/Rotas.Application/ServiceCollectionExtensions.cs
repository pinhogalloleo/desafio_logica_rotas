using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Application.UseCases.Viagens.GetAll;
using Rotas.Application.Services;

namespace Rotas.Application.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupUseCasesAndServicesFacade(this IServiceCollection services)
    {
        services.AddScoped<InsertViagemUseCase>();
        services.AddScoped<UpdateViagemUseCase>();
        services.AddScoped<DeleteViagemUseCase>();
        services.AddScoped<GetByIdViagemUseCase>();
        services.AddScoped<GetAllViagemUseCase>();
        services.AddScoped<ViagemService>();
        return services;
    }
}

