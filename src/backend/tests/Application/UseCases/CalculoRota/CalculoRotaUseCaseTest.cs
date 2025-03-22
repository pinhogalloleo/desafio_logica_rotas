
using Moq;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Application.UseCases.CalculoRota.Dto;
using Rotas.Domain.Entities.Grafo;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.CalculoRota;

public class CalculoRotaUseCaseTest
{
    [Fact]
    public async Task TestExecute_ReturnEntityNotNull()
    {
        // Arrange
        var calculoRotaService = new Mock<ICalculoRotaService>();
        var useCase = new CalculoRotaUseCase(calculoRotaService.Object);
        var dto = new CalculoRotaDto { Origem = "A", Destino = "B" };
        var rotaInput = new Rota() { Caminho = ["A", "B"], CustoTotal = 10 };

        calculoRotaService.Setup(x => x.CalcularRotaAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(rotaInput);

        // Act
        Rota? rotaReturn = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.NotNull(rotaReturn);
    }

    [Fact]
    public async Task TestExecute_ReturnEntityNull()
    {
        // Arrange
        var calculoRotaService = new Mock<ICalculoRotaService>();
        var useCase = new CalculoRotaUseCase(calculoRotaService.Object);
        var dto = new CalculoRotaDto { Origem = "A", Destino = "B" };

        calculoRotaService.Setup(x => x.CalcularRotaAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Rota?)null);

        // Act
        Rota? rotaReturn = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.Null(rotaReturn);
    }
}
