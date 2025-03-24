
using Moq;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.DeslocamentoCrud.GetById;

public class GetByIdDeslocamentoUseCaseTest
{
    [Fact]
    public async Task Test_GetByIdDeslocamento_ReturnsDeslocamento_WhenValidId()
    {
        // Arrange        
        var deslocamento = new Rotas.Domain.Entities.Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 100
        };
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Deslocamento>>();
        repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(deslocamento);
        var useCase = new GetByIdDeslocamentoUseCase(repository.Object);

        var dto = new GetByIdDeslocamentoDto() { Id = deslocamento.Id };

        // Act
        var result = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(deslocamento.Id, result.Id);
        Assert.Equal(deslocamento.Origem, result.Origem);
        Assert.Equal(deslocamento.Destino, result.Destino);
        Assert.Equal(deslocamento.Custo, result.Custo);
    }

    [Fact]
    public async Task Test_GetByIdDeslocamento_RaisesNaoEncontradoException_WhenInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Deslocamento>>();
        var useCase = new GetByIdDeslocamentoUseCase(repository.Object);
        var dto = new GetByIdDeslocamentoDto() { Id = 1 };

        // Act
        // Assert
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => useCase.ExecuteAsync(dto));
        Assert.Contains("deslocamento não encontrada", exception.Message.ToLowerInvariant());
    }

}
