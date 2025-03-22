using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;
using Tests.Domain.Entities;

namespace Tests.Application.UseCases.Viagem;

public class InsertViagemUseCaseTest
{
    [Fact]
    public async Task Test_AddViagem_Success_WhenValidViagem()
    {
        // Arrange
        var dto = new InsertViagemDto()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 12.34m
        };

        var mockRepository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IViagemValidationService>(repository);
        var validationService = mockValidationService.Object;

        mockRepository.Setup(x => x.InsertAsync(It.IsAny<Rotas.Domain.Entities.Viagem>())).ReturnsAsync(1);

        var useCase = new InsertViagemUseCase(repository, validationService);

        // Act
        var createdId = await useCase.ExecuteAsync(dto);

        // Assert
        Assert.Equal(1, createdId);
    }


    [Fact]
    public async Task Test_AddViagem_RaisesValidacaoException_WhenInvalidEntity()
    {
        // Arrange
        var dto = new InsertViagemDto()
        {
            Origem = "AA",
            Destino = "",
            Custo = -1
        };

        var mockRepository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IViagemValidationService>(repository);
        var validationService = mockValidationService.Object;

        var useCase = new InsertViagemUseCase(repository, validationService);

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
        var dto = new InsertViagemDto()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 12.34m
        };

        var mockRepository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
        var repository = mockRepository.Object;
        var mockValidationService = new Mock<IViagemValidationService>();
        var validationService = mockValidationService.Object;

        var useCase = new InsertViagemUseCase(repository, validationService);
        mockRepository.Setup(x => x.InsertAsync(It.IsAny<Rotas.Domain.Entities.Viagem>())).ReturnsAsync(1);
        
        // Act

        var id1 = await useCase.ExecuteAsync(dto);
        var id2 = 0;

        mockValidationService.Setup(x =>
            x.ValidateDuplicityAsync(dto.Origem, dto.Destino, null))
            .ThrowsAsync(new DuplicidadeException("Já existe outra Viagem cadastrada com origem AAA e destino BBB"));

        async Task act2() => id2 = await useCase.ExecuteAsync(dto);

        // Assert        
        await Assert.ThrowsAsync<DuplicidadeException>(act2);
        Assert.Equal(1, id1);
        Assert.Equal(0, id2);
    }

}
