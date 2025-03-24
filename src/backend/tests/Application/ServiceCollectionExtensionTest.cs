
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Rotas.Application.DependencyInjection;
using Rotas.Application.Services;
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
        var insertDeslocamentoUseCase = serviceProvider.GetService<InsertDeslocamentoUseCase>();
        var updateDeslocamentoUseCase = serviceProvider.GetService<UpdateDeslocamentoUseCase>();
        var deleteDeslocamentoUseCase = serviceProvider.GetService<DeleteDeslocamentoUseCase>();
        var getByIdDeslocamentoUseCase = serviceProvider.GetService<GetByIdDeslocamentoUseCase>();
        var getAllDeslocamentoUseCase = serviceProvider.GetService<GetAllDeslocamentoUseCase>();
        var deslocamentoService = serviceProvider.GetService<DeslocamentoService>();
        var calculoRotaUseCase = serviceProvider.GetService<CalculoRotaUseCase>();
        var calculoRotaService = serviceProvider.GetService<CalculoRotaService>();
        var deslocamentoValidationService = serviceProvider.GetService<DeslocamentoValidationService>();

        // Assert        
        Assert.NotNull(insertDeslocamentoUseCase);
        Assert.NotNull(updateDeslocamentoUseCase);
        Assert.NotNull(deleteDeslocamentoUseCase);
        Assert.NotNull(getByIdDeslocamentoUseCase);
        Assert.NotNull(getAllDeslocamentoUseCase);
        Assert.NotNull(deslocamentoService);
        Assert.NotNull(calculoRotaUseCase);
        Assert.NotNull(calculoRotaService);
        Assert.NotNull(deslocamentoValidationService);
    }
}
