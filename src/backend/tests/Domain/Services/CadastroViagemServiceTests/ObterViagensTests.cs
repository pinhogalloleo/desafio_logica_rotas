using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace tests.Domain.Services.CadastroViagemServiceTests
{
    public class GetTests
    {
        [Fact]
        public async Task Test_ObterViagens_ReturnsListOfViagens()
        {
            // Arrange
            var repository = new Mock<IRepositoryCrud<Viagem>>();
            var viagens = new List<Viagem>
        {
            new Viagem { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 100 },
            new Viagem { Id = 2, Origem = "CCC", Destino = "DDD", Custo = 200 }
        };
            repository.Setup(x => x.GetAllAsync()).ReturnsAsync(viagens);
            var service = new CadastroViagemService(repository.Object);

            // Act
            var result = await service.ObterViagens();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("AAA", result[0].Origem);
            Assert.Equal("CCC", result[1].Origem);
        }

    }
}