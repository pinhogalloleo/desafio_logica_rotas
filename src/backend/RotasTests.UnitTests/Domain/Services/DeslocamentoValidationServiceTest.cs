
using Rotas.Domain.Services;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.DataAccess.FileDataAccess;
using Tests.Domain.Entities;

namespace Tests.Domain.Services;

public class DeslocamentoValidationServiceTest
{
    [Fact]
    public async Task Validation_NoDuplicity()
    {
        // Arrange
        var path = "testValidDuplicity_NoDup.json";
        using var repository = new RepositoryCrudDeslocamento(path);
        var service = new DeslocamentoValidationService(repository);

        // Act
        var exception = await Record.ExceptionAsync(async () => await service.ValidateDuplicityAsync("AAA", "BBB"));

        // Assert
        Assert.Null(exception);

        // clean up
        File.Delete(path);
    }


    [Fact]
    public async Task Validation_There_IS_Duplicity_WhithoutId()
    {
        // Arrange
        var path = "testValidDuplicity_NoId.json";
        using var repository = new RepositoryCrudDeslocamento(path);
        var service = new DeslocamentoValidationService(repository);
        var entity = DeslocamentoEntityFactory.FakeList(1)[0];
        await repository.InsertAsync(entity);

        // Act
        var otherSimilarEntity = new Deslocamento
        {
            Origem = entity.Origem,
            Destino = entity.Destino,
            Custo = entity.Custo
        };
        var exception = await Record.ExceptionAsync(() => service.ValidateDuplicityAsync(otherSimilarEntity.Origem, otherSimilarEntity.Destino));

        // Assert
        var expectedMsg = $"Já existe outra Deslocamento cadastrada com origem {entity.Origem} e destino {entity.Destino}";
        Assert.NotNull(exception);
        Assert.IsType<DuplicidadeException>(exception);
        Assert.Contains(expectedMsg, exception.Message);

        // clean up
        File.Delete(path);
    }


    [Fact]
    public async Task Validation_There_IS_Duplicity_WhithId()
    {
        // Arrange
        var path = "testValidDuplicity_WithId.json";
        using var repository = new RepositoryCrudDeslocamento(path);
        var service = new DeslocamentoValidationService(repository);
        var newList = DeslocamentoEntityFactory.FakeList(2);
        await repository.InsertAsync(newList[0]);
        await repository.InsertAsync(newList[1]);

        // Act
        var firstEntity = newList[0];
        var savedList = await repository.GetAllAsync();
        var updEntity = savedList[1];
        updEntity.Origem = firstEntity.Origem;
        updEntity.Destino = firstEntity.Destino;

        var exception = await Record.ExceptionAsync(() => service.ValidateDuplicityAsync(updEntity.Origem, updEntity.Destino, updEntity.Id));

        // Assert
        var expectedMsg = $"Já existe outra Deslocamento cadastrada com origem {firstEntity.Origem} e destino {firstEntity.Destino}";
        Assert.NotNull(exception);
        Assert.IsType<DuplicidadeException>(exception);
        Assert.Contains(expectedMsg, exception.Message);

        // clean up
        File.Delete(path);
    }

}
