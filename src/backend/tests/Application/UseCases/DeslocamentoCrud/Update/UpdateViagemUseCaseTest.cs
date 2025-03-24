
using Moq;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.DeslocamentoCrud.Update;
public class UpdateDeslocamentoUseCaseTest
{
    [Fact]
    public async Task Test_UpdateDeslocamento_Success_WhenValidDeslocamento()
    {
        // Arrange
        Deslocamento? deslocamento = new Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var updateDto = new UpdateDeslocamentoDto()
        {
            Id = deslocamento.Id,
            Origem = "CCC",
            Destino = "DDD",
            Custo = 200
        };

        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), null)).Returns(Task.CompletedTask);

        var repository = new Mock<IRepositoryCrud<Deslocamento>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Deslocamento?>(deslocamento));
        repository.Setup(x => x.UpdateAsync(deslocamento)).Returns(Task.CompletedTask);

        var useCase = new UpdateDeslocamentoUseCase(repository.Object, mockValidationService.Object);

        // Act
        await useCase.ExecuteAsync(updateDto);

        // Assert
        repository.Verify(x => x.UpdateAsync(deslocamento), Times.Once);
        repository.Verify(x => x.GetByIdAsync(1), Times.Once);
    }



    // test exception when viagem is null
    [Fact]
    public async Task Test_UpdateDeslocamento_ThrowsArgumentNullException_WhenDeslocamentoIsNull()
    {
        // Arrange
        var dto = new UpdateDeslocamentoDto()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        var mockRepository = new Mock<IRepositoryCrud<Deslocamento>>();
        mockRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Deslocamento?>(null));

        var useCase = new UpdateDeslocamentoUseCase(mockRepository.Object, mockValidationService.Object);



        // Act
        var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => useCase.ExecuteAsync(dto));

        // Assert
        Assert.Contains("Deslocamento não encontrada pelo Id".ToLowerInvariant(), exception.Message.ToLowerInvariant());
    }



    // test update with invalid viagem, then check Validator result ValidacaoException for error messages
    [Fact]
    public async Task Test_UpdateDeslocamento_ThrowsValidacaoException_WhenDeslocamentoIsInvalid()
    {
        // Arrange
        var entidade = new Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 15
        };

        var dtoInvalid = new UpdateDeslocamentoDto()
        {
            Id = entidade.Id,
            Origem = "A",
            Destino = "",
            Custo = -150
        };

        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        mockValidationService.Setup(x => x.ValidateDuplicityAsync(It.IsAny<string>(), It.IsAny<string>(), entidade.Id)).Returns(Task.CompletedTask);

        var repository = new Mock<IRepositoryCrud<Deslocamento>>();
        repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Deslocamento?>(entidade));

        var useCase = new UpdateDeslocamentoUseCase(repository.Object, mockValidationService.Object);

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
    public async Task TestUpdate_FailWhenDuplicity()
    {
        // expects DuplicidadeException when there is a duplicated entity
        // Arrange
        var viagem = new Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150.00m
        };

        var dto = new UpdateDeslocamentoDto()
        {
            Id = viagem.Id,
            Origem = viagem.Origem,
            Destino = viagem.Destino,
            Custo = viagem.Custo
        };

        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        var validationService = mockValidationService.Object;

        var mockRepository = new Mock<IRepositoryCrud<Deslocamento>>();
        var repository = mockRepository.Object;

        var useCase = new UpdateDeslocamentoUseCase(repository, validationService);

        mockValidationService.Setup(x => x.ValidateDuplicityAsync(dto.Origem, dto.Destino, dto.Id)).ThrowsAsync(new DuplicidadeException("duplicidade"));
        mockRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult<Deslocamento?>(viagem));

        // Act
        async Task act() => await useCase.ExecuteAsync(dto);

        // Assert
        await Assert.ThrowsAsync<DuplicidadeException>(act);
    }


}
