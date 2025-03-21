
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

public class UpdateTests
{
    [Fact]
    public async Task Test_UpdateViagem_Success_WhenValidViagem()
    {
        // Arrange
        var viagem = new Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(viagem));
        repository.Setup(x => x.UpdateAsync(It.IsAny<Viagem>())).Returns(Task.FromResult(viagem));
        var service = new CadastroViagemService(repository.Object);

        // Act
        await service.UpdateViagem(viagem);

        // Assert
        repository.Verify(x => x.UpdateAsync(viagem), Times.Once);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
    }



    // test exception when viagem is null
    [Fact]
    public async Task Test_UpdateViagem_ThrowsArgumentNullException_WhenViagemIsNull()
    {
        // Arrange
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var service = new CadastroViagemService(repository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateViagem(null));

        // Assert
        Assert.Contains("objeto viagem não pode ser nulo", exception.Message.ToLowerInvariant());
    }



    [Fact] // test NaoEncontradoException when viagem not found
    public async Task Test_UpdateViagem_ThrowsNaoEncontradoException_WhenViagemNotFound()
    {
        // Arrange
        var viagem = new Viagem()
        {
            Id = 1111,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };
        var repository = new Mock<IRepositoryCrud<Viagem>>();
        var service = new CadastroViagemService(repository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => service.UpdateViagem(viagem));

        // Assert
        Assert.Contains("viagem não encontrada", exception.Message.ToLowerInvariant());
    }


    // test update with invalid viagem, then check Validator result ValidacaoException for error messages
    [Fact]
    public async Task Test_UpdateViagem_ThrowsValidacaoException_WhenViagemIsInvalid()
    {
        // Arrange
        var viagemOriginal = new Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 15
        };

        var viagemChanged = new Viagem()
        {
            Id = 1,
            Origem = "A",
            Destino = "",
            Custo = -150
        };

        var repository = new Mock<IRepositoryCrud<Viagem>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(viagemOriginal));

        var service = new CadastroViagemService(repository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<ValidacaoException>(() => service.UpdateViagem(viagemChanged));

        // Assert
        Assert.Contains("Origem deve ter pelo menos 3 letras".ToLowerInvariant(), exception.Message.ToLowerInvariant());
        Assert.Contains("Destino é obrigatório".ToLowerInvariant(), exception.Message.ToLowerInvariant());
        Assert.Contains("custo deve ser mínimo de 0.01 e máximo de 999.99", exception.Message.ToLowerInvariant());
    }



}//..class

