
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;

namespace Tests.Application.UseCases.DeslocamentoCrud.Insert;

public class InsertDeslocamentoDtoTest
{
    [Fact]
    public void TestInstantiationOfInsertViagemDto()
    {
        // Arrange
        InsertDeslocamentoDto dto;

        // Act
        dto = new InsertDeslocamentoDto() { Origem = "AAA", Destino = "BBB", Custo = 100 };

        // Assert
        Assert.Equal("AAA", dto.Origem);
        Assert.Equal("BBB", dto.Destino);
        Assert.Equal(100, dto.Custo);
    }
}
