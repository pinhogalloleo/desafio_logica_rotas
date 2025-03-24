
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;

namespace Tests.Application.UseCases.DeslocamentoCrud.GetById;

public class GetByIdDeslocamentoDtoTest
{
    [Fact]
    public void TestInstantiateGetByIdDeslocamentoDto()
    {
        // Arrange
        var dto = new GetByIdDeslocamentoDto();

        // Act
        // Assert
        Assert.NotNull(dto);
    }
}
