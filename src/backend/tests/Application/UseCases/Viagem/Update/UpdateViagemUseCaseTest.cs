
using Moq;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Tests.Application.UseCases.Viagem;
public class UpdateViagemUseCaseTest
{
    [Fact]
    public async Task Test_UpdateViagem_Success_WhenValidViagem()
    {
        // Arrange
        Rotas.Domain.Entities.Viagem? viagem = new Rotas.Domain.Entities.Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var updateDto = new UpdateViagemDto()
        {
            Id = viagem.Id,
            Origem = "CCC",
            Destino = "DDD",
            Custo = 200
        };

        var mockValidationService = new Mock<IViagemValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), null)).Returns(Task.CompletedTask);

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Rotas.Domain.Entities.Viagem?>(viagem));
        repository.Setup(x => x.UpdateAsync(viagem)).Returns(Task.CompletedTask);

        var useCase = new UpdateViagemUseCase(repository.Object, mockValidationService.Object);

        // Act
        await useCase.ExecuteAsync(updateDto);

        // Assert
        repository.Verify(x => x.UpdateAsync(viagem), Times.Once);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
    }



    // test exception when viagem is null
    [Fact]
    public async Task Test_UpdateViagem_ThrowsArgumentNullException_WhenViagemIsNull()
    {
        // Arrange
        UpdateViagemDto dto = new UpdateViagemDto()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var mockValidationService = new Mock<IViagemValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), null)).Returns(Task.CompletedTask);

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var useCase = new UpdateViagemUseCase(repository.Object, mockValidationService.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => useCase.ExecuteAsync(dto));

        // Assert
        Assert.Contains("Viagem não encontrada pelo Id", exception.Message.ToLowerInvariant());
    }



    // test update with invalid viagem, then check Validator result ValidacaoException for error messages
    [Fact]
    public async Task Test_UpdateViagem_ThrowsValidacaoException_WhenViagemIsInvalid()
    {
        // Arrange
        var entidade = new Rotas.Domain.Entities.Viagem()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 15
        };

        var dtoInvalid = new UpdateViagemDto()
        {
            Id = entidade.Id,
            Origem = "A",
            Destino = "",
            Custo = -150
        };

        var mockValidationService = new Mock<IViagemValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), entidade.Id)).Returns(Task.CompletedTask);

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Rotas.Domain.Entities.Viagem?>(entidade));

        var useCase = new UpdateViagemUseCase(repository.Object, mockValidationService.Object);

        // Act
        var exception = await Assert.ThrowsAsync<ValidacaoException>(() => useCase.ExecuteAsync(dtoInvalid));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ValidacaoException>(exception);
        Assert.Contains("origem", exception.Message.ToLowerInvariant());
        Assert.Contains("destino", exception.Message.ToLowerInvariant());
        Assert.Contains("custo", exception.Message.ToLowerInvariant());
    }


    [Fact]
    public async Task TestInsert_FailWhenDuplicity()
    {
        // expects DuplicidadeException when there is a duplicated entity
        // Arrange
        var dto = new UpdateViagemDto()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150.00m
        };

        var mockValidationService = new Mock<IViagemValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), null)).ThrowsAsync(new DuplicidadeException("duplicidade"));

        var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var useCase = new UpdateViagemUseCase(repository.Object, mockValidationService.Object);

        // Act

        async Task act() => await useCase.ExecuteAsync(dto);

        // Assert
        await Assert.ThrowsAsync<DuplicidadeException>(act);        
    }


}
