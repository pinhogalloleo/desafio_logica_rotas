using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.DataAccess.FileDataAccess;
using Rotas.Domain.Entities;
using tests.Domain.Entities;

namespace tests.Infra.DataAccess.FileDataAccess;

public class TestInsert
{
    [Fact]
    public async Task TestRepositoryCrudViagem_AddedSuccessfully()
    {
        // Arrange
        var path = "testInfraFileDataAccessInsertSuccess.json";
        var viagem = ViagemEntityFactory.FakeList(1)[0];

        // Act
        using var repository = new RepositoryCrudViagem(path);
        var lista = await repository.GetAllAsync();
        var qtd = lista.Count;
        var id = await repository.InsertAsync(viagem);

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
    public async Task TestRepositoryCrudViagem_AddedSuccessfullyWithMultipleItems()
    {
        // Arrange
        var expectedQtd = 3;
        var path = "testInfraFileDataAccessInsertSuccessMultiple.json";
        var viagens = ViagemEntityFactory.FakeList(expectedQtd);

        // Act
        using var repository = new RepositoryCrudViagem(path);
        var idsList = new List<int>();
        foreach (var viagem in viagens)
        {
            var id = await repository.InsertAsync(viagem);
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
    public async Task TestRepositoryCrudViagem_InsertAsyncWithNullEntity()
    {
        // Arrange
        var path = "testInfraFileDataAccessInsertNullEntity.json";
        Viagem? viagem = null;

        // Act
        using var repository = new RepositoryCrudViagem(path);
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.InsertAsync(viagem));

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(ex);
        Assert.Equal("entity", ex.ParamName);
        Assert.Contains("Objeto viagem não pode ser nulo".ToLowerInvariant(), ex.Message.ToLowerInvariant());

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


}//...class
