
using Microsoft.Extensions.DependencyInjection;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Application.UseCases.DeslocamentoCrud.GetAll;
using Rotas.Application.Services;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;
using Rotas.Application.UseCases;
using Rotas.Domain.Entities;
using Rotas.Domain.Entities.Grafo;

namespace Rotas.Application.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupUseCasesAndServicesFacade(this IServiceCollection services)
    {
        // for validation, required inside use cases Insert and Update
        services.AddScoped<IDeslocamentoDuplicityValidationService, DeslocamentoValidationService>();

        // crud use cases
        services.AddScoped<IUseCase<InsertDeslocamentoDto, Task<int>>, InsertDeslocamentoUseCase>();
        services.AddScoped<IUseCase<UpdateDeslocamentoDto, Task>, UpdateDeslocamentoUseCase>();
        services.AddScoped<IUseCase<DeleteDeslocamentoDto, Task>, DeleteDeslocamentoUseCase>();
        services.AddScoped<IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>>, GetByIdDeslocamentoUseCase>();
        services.AddScoped<IUseCase<Task<IEnumerable<Deslocamento>>>, GetAllDeslocamentoUseCase>();

        services.AddScoped<IDeslocamentoService, DeslocamentoService>();
        services.AddScoped<ICalculoMelhorRotaService, CalculoMelhorRotaService>();

        // calculo de rota
        services.AddScoped<IUseCase<CalculoRotaDto, Task<Rota?>>, CalculoRotaUseCase>();
        services.AddScoped<ICalculoRotaService, CalculoRotaService>();

        return services;
    }


}

