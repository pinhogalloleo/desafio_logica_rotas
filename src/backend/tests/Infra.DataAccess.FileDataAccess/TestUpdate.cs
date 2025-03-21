using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.DataAccess.FileDataAccess;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using tests.Domain.Entities;

namespace tests.Infra.DataAccess.FileDataAccess;

public class TestUpdate
{
    [Fact]
    public async Task TestUpdate_FailWhenNoValidIndex()
    {
        // Arrange
        var path = "testUpdFail.json";
        using var repository = new RepositoryCrudViagem(path);
        var viagem = ViagemEntityFactory.FakeList(1)[0];
        viagem.Id = 1000; // neither this entity was added, nor it exists id 1000

        // Act + Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(async () => await repository.UpdateAsync(viagem));

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


    // test update success when entity exists
    [Fact]
    public async Task TestUpdate_Success()
    {
        // Arrange
        var path = "testUpdSuccess.json";
        using var repository = new RepositoryCrudViagem(path);
        var viagem = new Viagem
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 10
        };
        var newId = await repository.InsertAsync(viagem);

        // Act - intermediary
        var retrievedAfterCreated = await repository.GetByIdAsync(newId);

        // Assert intermediary
        Assert.NotNull(retrievedAfterCreated);
        Assert.Equal(newId, retrievedAfterCreated.Id);
        Assert.Equal(viagem.Origem, retrievedAfterCreated.Origem);
        Assert.Equal(viagem.Destino, retrievedAfterCreated.Destino);
        Assert.Equal(viagem.Custo, retrievedAfterCreated.Custo);

        // Act - updating
        retrievedAfterCreated.Origem = "CCC";
        retrievedAfterCreated.Destino = "DDD";
        retrievedAfterCreated.Custo = 20;
        var updated = await repository.UpdateAsync(viagem);

        // Assert - checking after update
        Assert.NotNull(updated);
        Assert.Equal(viagem.Id, updated.Id);
        Assert.Equal(viagem.Origem, updated.Origem);
        Assert.Equal(viagem.Destino, updated.Destino);
        Assert.Equal(viagem.Custo, updated.Custo);

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


    [Fact]
    public async Task TestUpdate_FailWhenEntityIsNull()
    {
        // Arrange
        var path = "testUpdFailNull.json";
        using var repository = new RepositoryCrudViagem(path);

        // Act
        var exception = await Record.ExceptionAsync(async () => await repository.UpdateAsync(null));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Contains("Objeto viagem não pode ser nulo".ToLowerInvariant(), exception.Message.ToLowerInvariant());

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


}//..class
