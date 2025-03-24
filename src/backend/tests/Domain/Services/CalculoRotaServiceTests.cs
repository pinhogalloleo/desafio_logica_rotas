
using Rotas.Domain.Services;
using Moq;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Tests.Domain.Entities;
using Rotas.Domain.Entities.Grafo;
using Rotas.DataAccess.FileDataAccess;

namespace Tests.Domain.Services;
public class CalculoRotaServiceTests
{
    [Fact]
    public async Task Test_CalculateRoute_RaiseNaoEncontradoException_WhenNoRoutesFound()
    {
        // Arrange
        var mockRepository = new Mock<IRepositoryCrud<Deslocamento>>();
        mockRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<Deslocamento>()));
        var service = new CalculoRotaService(Mock.Of<IRepositoryCrud<Deslocamento>>());

        // Act
        async Task act() => await service.CalcularRotaAsync("AAA", "BBB");

        // Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(act);
    }


    [Fact]
    public async Task Test_CalculateRoute_ReturnsExpectedResult()
    {
        // Arrange
        var path = "testCalcRouteSuccess.json";
        using var repository = new RepositoryCrudDeslocamento(path);

        var deslocamentos = DeslocamentoEntityFactory.FakeList(3);

        deslocamentos[0].Origem = "AAA";
        deslocamentos[0].Destino = "BBB";
        deslocamentos[0].Custo = 10;

        deslocamentos[1].Origem = "BBB";
        deslocamentos[1].Destino = "CCC";
        deslocamentos[1].Custo = 10;

        deslocamentos[2].Origem = "AAA";
        deslocamentos[2].Destino = "CCC";
        deslocamentos[2].Custo = 30;

        await repository.InsertAsync(deslocamentos[0]);
        await repository.InsertAsync(deslocamentos[1]);
        await repository.InsertAsync(deslocamentos[2]);

        var service = new CalculoRotaService(repository);

        // Act
        Rota? rota = await service.CalcularRotaAsync("AAA", "CCC");

        // Assert
        Assert.NotNull(rota);
        Assert.Equal(20, rota.CustoTotal);
        Assert.Equal(3, rota.Caminho.Count);
        Assert.Equal("AAA", rota.Caminho[0]);
        Assert.Equal("BBB", rota.Caminho[1]);
        Assert.Equal("CCC", rota.Caminho[2]);

        // clean up
        File.Delete(path);        
    }
    
}
