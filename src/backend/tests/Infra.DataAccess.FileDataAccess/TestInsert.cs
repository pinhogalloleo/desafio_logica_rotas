
using Rotas.DataAccess.FileDataAccess;
using Rotas.Domain.Entities;
using Tests.Domain.Entities;

namespace Tests.Infra.DataAccess.FileDataAccess;

public class TestInsert
{
    [Fact]
    public async Task TestRepositoryCrudDeslocamento_AddedSuccessfully()
    {
        // Arrange
        var path = "testInfraFileDataAccessInsertSuccess.json";
        var deslocamento = DeslocamentoEntityFactory.FakeList(1)[0];

        // Act
        using var repository = new RepositoryCrudDeslocamento(path);
        var lista = await repository.GetAllAsync();
        var qtd = lista.Count;
        var id = await repository.InsertAsync(deslocamento);

        // Assert
        Assert.NotNull(repository);
        Assert.True(File.Exists(path));
        Assert.Equal(qtd + 1, lista.Count);
        Assert.Equal(qtd + 1, id);

        // clean up
        repository.Dispose();
        File.Delete(path);
    }

    [Fact]
    public async Task TestRepositoryCrudDeslocamento_AddedSuccessfullyWithMultipleItems()
    {
        // Arrange
        var expectedQtd = 3;
        var path = "testInfraFileDataAccessInsertSuccessMultiple.json";
        var viagens = DeslocamentoEntityFactory.FakeList(expectedQtd);

        // Act
        using var repository = new RepositoryCrudDeslocamento(path);
        var idsList = new List<int>();
        foreach (var deslocamento in viagens)
        {
            var id = await repository.InsertAsync(deslocamento);
            idsList.Add(id);
        }
        var lista = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(repository);
        Assert.Equal(expectedQtd, lista.Count);
        Assert.Equal(expectedQtd, idsList.Count);
        Assert.True(File.Exists(path));
        foreach (var id in idsList)
            Assert.Contains(id, lista.Select(v => v.Id));

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


    [Fact]
    // test InsertAsync method with null entity, raising ArgumentNullException
    public async Task TestRepositoryCrudDeslocamento_InsertAsyncWithNullEntity()
    {
        // Arrange
        var path = "testInfraFileDataAccessInsertNullEntity.json";
        Deslocamento? deslocamento = null;

        // Act
        using var repository = new RepositoryCrudDeslocamento(path);
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.InsertAsync(deslocamento));

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(ex);
        Assert.Equal("entity", ex.ParamName);
        Assert.Contains("Objeto deslocamento não pode ser nulo".ToLowerInvariant(), ex.Message.ToLowerInvariant());

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


}//...class
