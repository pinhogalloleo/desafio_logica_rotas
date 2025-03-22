using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.DataAccess.FileDataAccess;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Exceptions;
using Tests.Domain.Entities;

namespace Tests.Infra.DataAccess.FileDataAccess;

public class TestDelete
{
    [Fact]
    public async Task TestDelete_FailWhenNoValidIndex()
    {
        // Arrange
        var path = "testDelFail.json";
        using var repository = new RepositoryCrudViagem(path);
        var viagem = ViagemEntityFactory.FakeList(1)[0];
        viagem.Id = 1000; // neither this entity was added, nor it exists id 1000

        // Act + Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(async () => await repository.DeleteAsync(viagem.Id));

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


    // test delete success when entity exists
    [Fact]
    public async Task TestDelete_Success()
    {
        // Arrange
        var path = "testDelSuccess.json";
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
        var listAfterCreated = await repository.GetAllAsync();

        // Assert intermediary
        Assert.NotNull(retrievedAfterCreated);
        Assert.True(listAfterCreated.Count == 1);
        Assert.Equal(newId, retrievedAfterCreated.Id);
        Assert.Equal(viagem.Origem, retrievedAfterCreated.Origem);
        Assert.Equal(viagem.Destino, retrievedAfterCreated.Destino);
        Assert.Equal(viagem.Custo, retrievedAfterCreated.Custo);

        // Act - deleting, and trying to retrieve it
        await repository.DeleteAsync(newId);

        var listAfterDeleted = await repository.GetAllAsync();
        Viagem? retrievedAfterDeleted = null;
        var exception = await Record.ExceptionAsync(async () => retrievedAfterDeleted = await repository.GetByIdAsync(newId));
        

        // Assert - checking after update
        Assert.Null(exception);
        Assert.Null(retrievedAfterDeleted);
        Assert.True(listAfterDeleted.Count == 0);

        // clean up
        repository.Dispose();
        File.Delete(path);
    }

}
