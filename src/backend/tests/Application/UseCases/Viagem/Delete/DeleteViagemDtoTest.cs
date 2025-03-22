
using Rotas.Application.UseCases.Viagens.Delete;

namespace Tests.Application.UseCases.Viagem.Delete;
public class DeleteViagemDtoTest
{
    [Fact]
    public void InstantiateDeleteViagemDto()
    {
        // Arrange
        DeleteViagemDto deleteViagemDto;

        // Act
        deleteViagemDto = new DeleteViagemDto() { Id = 1 };
        // Assert
        Assert.NotNull(deleteViagemDto);
    }
}
