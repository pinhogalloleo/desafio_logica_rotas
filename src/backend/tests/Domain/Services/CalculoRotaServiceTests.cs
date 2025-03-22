
using Rotas.Domain.Services;
using Moq;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Tests.Domain.Entities;
using Rotas.DataAccess.FileDataAccess;
using Rotas.Domain.Entities.Grafo;


namespace Tests.Domain.Services;
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
        async Task act() => await service.CalcularRotaAsync("AAA", "BBB");

        // Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(act);
    }


    [Fact]
    public async Task Test_CalculateRoute_ReturnsExpectedResult()
    {
        // Arrange
        var path = "testCalcRouteSuccess.json";
        using var repository = new RepositoryCrudViagem(path);

        var viagens = ViagemEntityFactory.FakeList(3);

        viagens[0].Origem = "AAA";
        viagens[0].Destino = "BBB";
        viagens[0].Custo = 10;

        viagens[1].Origem = "BBB";
        viagens[1].Destino = "CCC";
        viagens[1].Custo = 10;

        viagens[2].Origem = "AAA";
        viagens[2].Destino = "CCC";
        viagens[2].Custo = 30;

        await repository.InsertAsync(viagens[0]);
        await repository.InsertAsync(viagens[1]);
        await repository.InsertAsync(viagens[2]);

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
