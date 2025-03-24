

using Rotas.Application.UseCases.DeslocamentoCrud.Update;

namespace Tests.Application.UseCases.DeslocamentoCrud.Update;

public class UpdateViagemDtoTest
{
    [Fact]
    public void InstantiateUpdateViagemDtoTest()
    {
        // Arrange
        UpdateDeslocamentoDto dto;

        // Act
        dto = new UpdateDeslocamentoDto() { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 150 };

        // Assert
        Assert.NotNull(dto);
    }
}
