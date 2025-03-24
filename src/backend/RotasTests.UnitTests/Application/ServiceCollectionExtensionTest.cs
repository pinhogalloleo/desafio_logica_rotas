
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Rotas.Application.DependencyInjection;
using Rotas.Application.Services;
using Rotas.Application.UseCases;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetAll;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Tests.Application;
public class ServiceCollectionExtensionTest
{
    [Fact]
    public void TestSetupFileDataAccess_Success()
    {
        // Arrange
        var services = new ServiceCollection();

        // inject IRepositoryCrud<Deslocamento>, because DeslocamentoService needs it, and a Mock IRepositoryCrud<Deslocamento> as implementation
        services.AddScoped<IRepositoryCrud<Deslocamento>>(x => new Mock<IRepositoryCrud<Deslocamento>>().Object);

        // Act
        services.SetupUseCasesAndServicesFacade();
        var serviceProvider = services.BuildServiceProvider();

        // Assert        
        var insertDeslocamentoUseCase = serviceProvider.GetService<IUseCase<InsertDeslocamentoDto, Task<int>>>();
        var deslocamentoValidationService = serviceProvider.GetService<IDeslocamentoDuplicityValidationService>();

        // Assert        
        Assert.NotNull(services);
        Assert.True(services.Count > 0);
        Assert.NotNull(deslocamentoValidationService);
        Assert.NotNull(insertDeslocamentoUseCase);
    }
}
