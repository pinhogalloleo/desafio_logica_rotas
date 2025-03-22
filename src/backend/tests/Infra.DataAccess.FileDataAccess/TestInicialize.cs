
using Rotas.DataAccess.FileDataAccess;


namespace Tests.Infra.DataAccess.FileDataAccess;

public class TestInicialize
{
    // test initialization of Rotas.DataAccess.FileDataAccess.RepositoryCrudViagem
    [Fact]
    public void TestRepositoryCrudViagem_CreateFile_WhenFirstExecution()
    {
        // Arrange
        var path = "test1sExec.json";

        // Act
        using var repository = new RepositoryCrudViagem(path);

        // Assert
        Assert.NotNull(repository);
        Assert.True(File.Exists(path));

        // clean up
        repository.Dispose();
        File.Delete(path);
    }


    // test initialization of Rotas.DataAccess.FileDataAccess.RepositoryCrudViagem
    [Fact]
    public async Task TestRepositoryCrudViagem_LoadEmptyDataSecondExecution()
    {
        // Arrange
        var path = "test2ndExec.json";
        var repository = new RepositoryCrudViagem(path);

        // Act
        var lista = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(lista);
        Assert.True(File.Exists(path));

        // clean up
        repository.Dispose();

        // doing again to test if it loads the data
        repository = new RepositoryCrudViagem(path);
        lista = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(repository);
        Assert.NotNull(lista);
        Assert.Empty(lista);

        // clean up (again)
        repository.Dispose();

        // clean up
        File.Delete(path);
    }


}//..class
