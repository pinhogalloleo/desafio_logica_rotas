
using Rotas.Domain.Exceptions;
using Rotas.DataAccess.FileDataAccess;
using Tests.Domain.Entities;
using Rotas.Domain.Entities;

namespace Tests.Infra.DataAccess.FileDataAccess;

public class TestDelete
{
    [Fact]
    public async Task TestDelete_FailWhenNoValidIndex()
    {
        // Arrange
        var path = "testDelFail.json";
        using var repository = new RepositoryCrudDeslocamento(path);
        var deslocamento = DeslocamentoEntityFactory.FakeList(1)[0];
        deslocamento.Id = 1000; // neither this entity was added, nor it exists id 1000

        // Act + Assert
        await Assert.ThrowsAsync<NaoEncontradoException>(async () => await repository.DeleteAsync(deslocamento.Id));

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
        using var repository = new RepositoryCrudDeslocamento(path);
        var deslocamento = new Deslocamento
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 10
        };
        var newId = await repository.InsertAsync(deslocamento);

        // Act - intermediary
        var retrievedAfterCreated = await repository.GetByIdAsync(newId);
        var listAfterCreated = await repository.GetAllAsync();

        // Assert intermediary
        Assert.NotNull(retrievedAfterCreated);
        Assert.True(listAfterCreated.Count == 1);
        Assert.Equal(newId, retrievedAfterCreated.Id);
        Assert.Equal(deslocamento.Origem, retrievedAfterCreated.Origem);
        Assert.Equal(deslocamento.Destino, retrievedAfterCreated.Destino);
        Assert.Equal(deslocamento.Custo, retrievedAfterCreated.Custo);

        // Act - deleting, and trying to retrieve it
        await repository.DeleteAsync(newId);

        var listAfterDeleted = await repository.GetAllAsync();
        Deslocamento? retrievedAfterDeleted = null;
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
