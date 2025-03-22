
using Moq;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.Viagem.Delete;

public class DeleteViagemUseCaseTest
{
    [Fact]
    public async Task Test_DeleteViagem_Success_WhenValidId()
    {
        // Arrange
        var viagem = new Rotas.Domain.Entities.Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };
        var deleteDto = new DeleteViagemDto() { Id = viagem.Id };

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(viagem);
        repository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var useCase = new DeleteViagemUseCase(repository.Object);

        // Act
        await useCase.ExecuteAsync(deleteDto);

        // Assert
        repository.Verify(x => x.DeleteAsync(viagem.Id), Times.Once);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
    }


    // test Delete, that raises a NaoEncontradoException
    [Fact]
    public async Task Test_DeleteViagem_RaisesNaoEncontradoException_WhenInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var useCase = new DeleteViagemUseCase(repository.Object);
        var deleteDto = new DeleteViagemDto() { Id = 1 };
        repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Rotas.Domain.Entities.Viagem?)null);

        // Act
        async Task act() => await useCase.ExecuteAsync(deleteDto);

        // Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(act);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        repository.Verify(x => x.DeleteAsync(1), Times.Never);
    }


}