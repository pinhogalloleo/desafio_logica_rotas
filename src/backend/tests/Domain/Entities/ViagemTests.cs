
using Rotas.Domain.Entities;
using Xunit;

namespace Rotas.Tests.Domain.Entities;
public class ViagemTests
{
    [Fact]
    public void Test_Propriedade_Origem()
    {
        // Arrange
        var viagem = new Viagem()
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