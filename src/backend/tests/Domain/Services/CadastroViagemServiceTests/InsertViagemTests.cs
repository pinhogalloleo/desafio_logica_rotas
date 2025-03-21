using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace tests.Domain.Services.CadastroViagemServiceTests;

public class InsertViagemTests
{
    [Fact]
    public async Task Test_AddViagem_RaisesValidacaoException_WhenInvalidEntity()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var service = new CadastroViagemService(repository.Object);
        var viagem = new Viagem()
        {
            Origem = "AA",
            Destino = "",
            Custo = -1
        };

        // Act
        var exception = await Assert.ThrowsAsync<ValidacaoException>(() => service.InsertViagemAsync(viagem));

        // Assert
        Assert.Contains("origem", exception.Message.ToLowerInvariant());
        Assert.Contains("destino", exception.Message.ToLowerInvariant());
        Assert.Contains("custo", exception.Message.ToLowerInvariant());
    }

    [Fact]
    public async Task Test_AddViagem_Success_WhenValidViagem()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        repository.Setup(x => x.InsertAsync(It.IsAny<Viagem>())).ReturnsAsync(1);
        var service = new CadastroViagemService(repository.Object);
        var viagem = new Viagem()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 100
        };

        // Act
        var result = await service.InsertViagemAsync(viagem);

        // Assert
        Assert.Equal(1, result);
    }


    [Fact]
    public async Task TestInsert_FailWhenEntityIsNull()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var service = new CadastroViagemService(repository.Object);

        // Act
        var exception = await Record.ExceptionAsync(async () => await service.InsertViagemAsync(null));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Contains("Objeto Viagem não pode ser nulo".ToLowerInvariant(), exception.Message.ToLowerInvariant());
    }


}//..class
