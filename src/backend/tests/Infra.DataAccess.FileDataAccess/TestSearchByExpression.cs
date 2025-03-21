using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.DataAccess.FileDataAccess;
using Moq;
using System.IO;
using tests.Domain.Entities;

namespace tests.Infra.DataAccess.FileDataAccess;
public class TestSearchByExpression
{
    [Fact]
    public async Task TestRepositoryCrudViagem_SearchByExpression_ReturnsValue()
    {
        // Arrange
        var path = "testSearchByExpression.json";
        var viagens = ViagemEntityFactory.FakeList(3);
        var viagem = viagens[0];

        // Act
        using var repository = new RepositoryCrudViagem(path);
        foreach (var v in viagens)
            await repository.InsertAsync(v);

        var lista = await repository.SearchByExpressionAsync(v => v.Id == viagem.Id);

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(lista);
        Assert.Single(lista);
        Assert.Equal(viagem.Id, lista[0].Id);

        // clean up
        repository.Dispose();
        File.Delete(path);
    }

    // test search by expression returns nothing
    [Fact]
    public async Task TestRepositoryCrudViagem_SearchByExpression_ReturnsNothing()
    {
        // Arrange
        var path = "testSearchByExpressionNothing.json";
        var viagens = ViagemEntityFactory.FakeList(3);

        // Act
        using var repository = new RepositoryCrudViagem(path);
        foreach (var v in viagens)
            await repository.InsertAsync(v);

        var lista = await repository.SearchByExpressionAsync(v => v.Id == 0);

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(lista);
        Assert.Empty(lista);

        // clean up
        repository.Dispose();
        File.Delete(path);
    }

}
