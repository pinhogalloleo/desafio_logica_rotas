
using Rotas.Application.UseCases.Viagens.Insert;

namespace Tests.Application.UseCases.Viagem.Insert;

public class InsertViagemDtoTest
{
    [Fact]
    public void TestInstantiationOfInsertViagemDto()
    {
        // Arrange
        InsertViagemDto dto;

        // Act
        dto = new InsertViagemDto() { Origem = "AAA", Destino = "BBB", Custo = 100 };

        // Assert
        Assert.Equal("AAA", dto.Origem);
        Assert.Equal("BBB", dto.Destino);
        Assert.Equal(100, dto.Custo);
    }
}
