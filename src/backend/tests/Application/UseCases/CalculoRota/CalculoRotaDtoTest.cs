
using Moq;
using Rotas.Application.UseCases.CalculoRota.Dto;

namespace Tests.Application.UseCases.CalculoRota;

public class CalculoRotaDtoTest
{
    [Fact]
    public void InstantiateTest()
    {
        // Arrange\Act
        CalculoRotaDto dto = Mock.Of<CalculoRotaDto>();

        // Assert
        Assert.NotNull(dto);
    }
}
