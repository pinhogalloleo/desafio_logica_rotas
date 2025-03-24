
using Moq;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Domain.Entities;
using Rotas.Domain.Entities.Grafo;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Tests.Application.UseCases.CalculoRota;

public class CalculoRotaUseCaseTest
{
    [Fact]
    public async Task TestExecute_ReturnEntity_WhenDtoIsValid()
    {
        // Arrange        
        var dto = new CalculoRotaDto { Origem = "AAA", Destino = "BBB" };
        var rota = new Rota() { Caminho = ["AAA", "BBB"], CustoTotal = 10 };

        var mockCalculoRotaService = new Mock<ICalculoRotaService>();
        mockCalculoRotaService.Setup(x => x.CalcularRotaAsync(dto.Origem, dto.Destino)).ReturnsAsync(rota);
        var calculoRotaService = mockCalculoRotaService.Object;

        var useCase = new CalculoRotaUseCase(calculoRotaService);


        // Act
        Rota? rotaReturn = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.NotNull(rotaReturn);
    }

    [Fact]
    public async Task TestExecute_ReturnEntityNull()
    {
        // Arrange
        var dto = new CalculoRotaDto { Origem = "AAA", Destino = "BBB" };

        var mockCalculoRotaService = new Mock<ICalculoRotaService>();
        mockCalculoRotaService.Setup(x => x.CalcularRotaAsync(dto.Origem, dto.Destino)).ReturnsAsync((Rota?)null);

        var calculoRotaService = mockCalculoRotaService.Object;
        var useCase = new CalculoRotaUseCase(calculoRotaService);

        // Act
        Rota? rotaReturn = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.Null(rotaReturn);
    }
}
