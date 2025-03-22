using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Application.UseCases.Viagens.GetAll;
using Rotas.Domain.Interfaces;

namespace tests.Application.UseCases.Viagem
{
    public class GetAllViagemUseCaseTest
    {
                [Fact]
        public async Task Test_ObterViagens_ReturnsListOfViagens()
        {
            // Arrange
            var repository = new Mock<IRepositoryCrud<Rotas.Domain.Entities.Viagem>>();
            var viagens = new List<Rotas.Domain.Entities.Viagem>
        {
            new Rotas.Domain.Entities.Viagem { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 100 },
            new Rotas.Domain.Entities.Viagem { Id = 2, Origem = "CCC", Destino = "DDD", Custo = 200 }
        };
            repository.Setup(x => x.GetAllAsync()).ReturnsAsync(viagens);
            var useCase = new GetAllViagemUseCase(repository.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("AAA", result[0].Origem);
            Assert.Equal("CCC", result[1].Origem);
        }
    }
}