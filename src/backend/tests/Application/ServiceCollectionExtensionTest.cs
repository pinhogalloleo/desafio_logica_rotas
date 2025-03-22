
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Rotas.Application.DependencyInjection;
using Rotas.Application.Services;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetAll;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
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

        // inject IRepositoryCrud<Viagem>, because ViagemService needs it, and a Mock IRepositoryCrud<Viagem> as implementation
        services.AddScoped<IRepositoryCrud<Viagem>>(x => new Mock<IRepositoryCrud<Viagem>>().Object);

        // Act
        services.SetupUseCasesAndServicesFacade();
        var serviceProvider = services.BuildServiceProvider();
        var insertViagemUseCase = serviceProvider.GetService<InsertViagemUseCase>();
        var updateViagemUseCase = serviceProvider.GetService<UpdateViagemUseCase>();
        var deleteViagemUseCase = serviceProvider.GetService<DeleteViagemUseCase>();
        var getByIdViagemUseCase = serviceProvider.GetService<GetByIdViagemUseCase>();
        var getAllViagemUseCase = serviceProvider.GetService<GetAllViagemUseCase>();
        var viagemService = serviceProvider.GetService<ViagemService>();
        var calculoRotaUseCase = serviceProvider.GetService<CalculoRotaUseCase>();
        var calculoRotaService = serviceProvider.GetService<ICalculoRotaService>();
        var viagemValidationService = serviceProvider.GetService<ViagemValidationService>();

        // Assert        
        Assert.NotNull(insertViagemUseCase);
        Assert.NotNull(updateViagemUseCase);
        Assert.NotNull(deleteViagemUseCase);
        Assert.NotNull(getByIdViagemUseCase);
        Assert.NotNull(getAllViagemUseCase);
        Assert.NotNull(viagemService);
        Assert.NotNull(calculoRotaUseCase);
        Assert.NotNull(calculoRotaService);
        Assert.NotNull(viagemValidationService);
    }
}
