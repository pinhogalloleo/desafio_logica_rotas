
using Microsoft.Extensions.DependencyInjection;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Application.UseCases.Viagens.GetAll;
using Rotas.Application.Services;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;
using Rotas.Domain.Entities;

namespace Rotas.Application.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupUseCasesAndServicesFacade(this IServiceCollection services)
    {
        // for validation, required inside use cases Insert and Update
        services.AddScoped<IViagemValidationService, ViagemValidationService>();

        // crud de viagens
        services.AddScoped<InsertViagemUseCase>();
        services.AddScoped<UpdateViagemUseCase>();
        services.AddScoped<DeleteViagemUseCase>();
        services.AddScoped<GetByIdViagemUseCase>();
        services.AddScoped<GetAllViagemUseCase>();
        services.AddScoped<ViagemService>();
        
        // calculo de rota
        services.AddScoped<CalculoRotaUseCase>();
        services.AddScoped<ICalculoRotaService, CalculoRotaService>();
        
        // for domain
        services.AddScoped<ViagemValidationService>();        
        
        return services;
    }
    
}

