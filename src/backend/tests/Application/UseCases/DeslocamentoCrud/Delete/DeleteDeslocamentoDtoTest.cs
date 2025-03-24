
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;

namespace Tests.Application.UseCases.DeslocamentoCrud.Delete;
public class DeleteDeslocamentoDtoTest
{
    [Fact]
    public void InstantiateDeleteViagemDto()
    {
        // Arrange
        DeleteDeslocamentoDto deleteViagemDto;

        // Act
        deleteViagemDto = new DeleteDeslocamentoDto() { Id = 1 };
        // Assert
        Assert.NotNull(deleteViagemDto);
    }
}
