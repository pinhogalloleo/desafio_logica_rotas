using Rotas.Domain.Entities;
using Rotas.Domain.Entities.Grafo;

namespace Tests.Domain.Entities;

public class GrafoTest
{
    [Fact]
    public void Test_Grafo_ThrowsArgumentNullException_WhenViagensIsNull()
    {
        // Arrange
        List<Viagem>? viagens = null;

        // Act
        void act() => new Grafo(viagens!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

}
