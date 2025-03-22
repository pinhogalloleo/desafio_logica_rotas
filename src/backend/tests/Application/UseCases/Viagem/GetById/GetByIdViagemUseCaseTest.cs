using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.Viagem;

public class GetByIdViagemUseCaseTest
{
    [Fact]
    public async Task Test_GetByIdViagem_ReturnsViagem_WhenValidId()
    {
        // Arrange        
        var viagem = new Rotas.Domain.Entities.Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 100
        };
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(viagem);
        var useCase = new GetByIdViagemUseCase(repository.Object);

        var dto = new GetByIdViagemDto() { Id = viagem.Id };

        // Act
        var result = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(viagem.Id, result.Id);
        Assert.Equal(viagem.Origem, result.Origem);
        Assert.Equal(viagem.Destino, result.Destino);
        Assert.Equal(viagem.Custo, result.Custo);
    }

    [Fact]
    public async Task Test_GetByIdViagem_RaisesNaoEncontradoException_WhenInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var useCase = new GetByIdViagemUseCase(repository.Object);
        var dto = new GetByIdViagemDto() { Id = 1 };

        // Act
        // Assert
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => useCase.ExecuteAsync(dto));
        Assert.Contains("viagem não encontrada", exception.Message.ToLowerInvariant());
    }

}
