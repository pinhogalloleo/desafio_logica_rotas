
using Rotas.DataAccess.FileDataAccess;
using Tests.Domain.Entities;

namespace Tests.Infra.DataAccess.FileDataAccess;
public class TestSearchByExpression
{
    [Fact]
    public async Task TestRepositoryCrudViagem_SearchByExpression_ReturnsValue()
    {
        // Arrange
        var path = "testSearchByExpression.json";
        var deslocamentos = DeslocamentoEntityFactory.FakeList(3);
        var deslocamento = deslocamentos[0];

        // Act
        using var repository = new RepositoryCrudDeslocamento(path);
        foreach (var v in deslocamentos)
            await repository.InsertAsync(v);

        var lista = await repository.SearchByExpressionAsync(v => v.Id == deslocamento.Id);

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(lista);
        Assert.Single(lista);
        Assert.Equal(deslocamento.Id, lista[0].Id);

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
        var deslocamentos = DeslocamentoEntityFactory.FakeList(3);

        // Act
        using var repository = new RepositoryCrudDeslocamento(path);
        foreach (var v in deslocamentos)
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
