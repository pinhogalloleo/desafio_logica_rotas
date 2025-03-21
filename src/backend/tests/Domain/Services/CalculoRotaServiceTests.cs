using Rotas.Domain.Services;
using Xunit;
using Moq;
using Bogus;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using System;
using tests.Domain.Entities;


namespace Rotas.Tests.Domain.Services;
public class CalculoRotaServiceTests
{
    [Fact]
    public async Task Test_CalculateRoute_RaiseNaoEncontradoException_WhenNoRoutesFound()
    {
        // Arrange
        var mockRepository = new Mock<IRepositoryCrud<Viagem>>();
        mockRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<Viagem>()));
        var service = new CalculoRotaService(Mock.Of<IRepositoryCrud<Viagem>>());

        // Act
        async Task act() => await service.LocalizarMelhorRota("AAA", "BBB");

        // Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(act);
    }

    [Fact]
    public async Task Test_CalculateRoute_ReturnsExpectedResult()
    {
        // Arrange
        var fakeList = ViagemEntityFactory.FakeList(3);
        var mockRepository = new Mock<IRepositoryCrud<Viagem>>();
        mockRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(fakeList));
        var service = new CalculoRotaService(mockRepository.Object);

        // Act
        var resultList = await service.LocalizarMelhorRota("A", "B");

        // Assert
        Assert.Equal(fakeList.Count, resultList.Count);
        mockRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}
