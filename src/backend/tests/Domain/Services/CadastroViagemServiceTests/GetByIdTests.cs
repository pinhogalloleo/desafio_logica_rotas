using Rotas.Domain.Services;
using Xunit;
using Moq;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rotas.Tests.Domain.Services;
public class GetByIdTests
{
    [Fact]
    public async Task Test_GetByIdViagem_ReturnsViagem_WhenValidId()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var viagem = new Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 100
        };
        repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(viagem);
        var service = new CadastroViagemService(repository.Object);

        // Act
        var result = await service.GetByIdViagem(1);

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
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var service = new CadastroViagemService(repository.Object);

        // Act
        // Assert
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => service.GetByIdViagem(1));
        Assert.Contains("viagem não encontrada", exception.Message.ToLowerInvariant());
    }


}