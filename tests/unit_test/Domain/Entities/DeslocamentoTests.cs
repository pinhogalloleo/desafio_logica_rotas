
using Rotas.Domain.Entities;

namespace Tests.Domain.Entities;
public class DeslocamentoTests
{
    [Fact]
    public void Test_Instantiate()
    {
        // Arrange
        var viagem = new Deslocamento()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 100
        };

        // Act
        // n/a

        // Assert
        Assert.Equal(1, viagem.Id);
        Assert.Equal("AAA", viagem.Origem);
        Assert.Equal("BBB", viagem.Destino);
        Assert.Equal(100, viagem.Custo);
    }
}