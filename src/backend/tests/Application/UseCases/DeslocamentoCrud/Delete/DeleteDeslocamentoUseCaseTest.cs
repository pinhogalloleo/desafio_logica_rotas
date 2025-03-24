
using Moq;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.DeslocamentoCrud.Delete;

public class DeleteDeslocamentoUseCaseTest
{
    [Fact]
    public async Task Test_DeleteDeslocamento_Success_WhenValidId()
    {
        // Arrange
        var deslocamento = new Rotas.Domain.Entities.Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };
        var deleteDto = new DeleteDeslocamentoDto() { Id = deslocamento.Id };

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Deslocamento>>();
        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(deslocamento);
        repository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var useCase = new DeleteDeslocamentoUseCase(repository.Object);

        // Act
        await useCase.ExecuteAsync(deleteDto);

        // Assert
        repository.Verify(x => x.DeleteAsync(deslocamento.Id), Times.Once);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
    }


    // test Delete, that raises a NaoEncontradoException
    [Fact]
    public async Task Test_DeleteDeslocamento_RaisesNaoEncontradoException_WhenInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Deslocamento>>();
        var useCase = new DeleteDeslocamentoUseCase(repository.Object);
        var deleteDto = new DeleteDeslocamentoDto() { Id = 1 };
        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Rotas.Domain.Entities.Deslocamento?)null);

        // Act
        async Task act() => await useCase.ExecuteAsync(deleteDto);

        // Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(act);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        repository.Verify(x => x.DeleteAsync(1), Times.Never);
    }


}