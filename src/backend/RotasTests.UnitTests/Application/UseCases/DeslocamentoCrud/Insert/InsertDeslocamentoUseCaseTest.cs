
using Moq;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Tests.Application.UseCases.DeslocamentoCrud.Insert;

public class InsertDeslocamentoUseCaseTest
{
    [Fact]
    public async Task Test_AddDeslocamento_Success_WhenValidDeslocamento()
    {
        // Arrange
        var dto = new InsertDeslocamentoDto()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 12.34m
        };

        var mockRepository = new Mock<IRepositoryCrud<Deslocamento>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        var validationService = mockValidationService.Object;

        mockRepository.Setup(x => x.InsertAsync(It.IsAny<Deslocamento>())).ReturnsAsync(1);

        var useCase = new InsertDeslocamentoUseCase(repository, validationService);

        // Act
        var createdId = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.Equal(1, createdId);
    }


    [Fact]
    public async Task Test_AddDeslocamento_RaisesValidacaoException_WhenInvalidEntity()
    {
        // Arrange
        var dto = new InsertDeslocamentoDto()
        {
            Origem = "AA",
            Destino = "",
            Custo = -1
        };

        var mockRepository = new Mock<IRepositoryCrud<Deslocamento>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        var validationService = mockValidationService.Object;

        var useCase = new InsertDeslocamentoUseCase(repository, validationService);

        // Act
        var exception = await Assert.ThrowsAsync<ValidacaoException>(() => useCase.ExecuteAsync(dto));

        // Assert
        Assert.Contains("origem", exception.Message.ToLowerInvariant());
        Assert.Contains("destino", exception.Message.ToLowerInvariant());
        Assert.Contains("custo", exception.Message.ToLowerInvariant());
    }


    [Fact]
    public async Task TestInsert_FailWhenDuplicity()
    {
        // expects DuplicidadeException when there is a duplicated entity
        // Arrange        
        var dto = new InsertDeslocamentoDto()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 12.34m
        };

        var mockRepository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Deslocamento>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IDeslocamentoDuplicityValidationService>();
        var validationService = mockValidationService.Object;

        var useCase = new InsertDeslocamentoUseCase(repository, validationService);
        mockRepository.Setup(x => x.InsertAsync(It.IsAny<Rotas.Domain.Entities.Deslocamento>())).ReturnsAsync(1);
        
        // Act

        var id1 = await useCase.ExecuteAsync(dto);
        var id2 = 0;

        mockValidationService.Setup(x =>
            x.ValidateDuplicityAsync(dto.Origem, dto.Destino, null))
            .ThrowsAsync(new DuplicidadeException("Já existe outra Deslocamento cadastrada com origem AAA e destino BBB"));

        async Task act2() => id2 = await useCase.ExecuteAsync(dto);

        // Assert        
        await Assert.ThrowsAsync<DuplicidadeException>(act2);
        Assert.Equal(1, id1);
        Assert.Equal(0, id2);
    }

}
