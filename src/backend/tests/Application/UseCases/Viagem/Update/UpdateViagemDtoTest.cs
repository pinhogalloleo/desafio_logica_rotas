
using Rotas.Application.UseCases.Viagens.Update;

namespace Tests.Application.UseCases.Viagem.Update
{
    public class UpdateViagemDtoTest
    {
        [Fact]
        public void InstantiateUpdateViagemDtoTest()
        {
            // Arrange
            UpdateViagemDto dto;

            // Act
            dto = new UpdateViagemDto() { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 150 };

            // Assert
            Assert.NotNull(dto);
        }
    }
}