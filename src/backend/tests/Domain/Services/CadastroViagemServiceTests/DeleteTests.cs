using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace tests.Domain.Services.CadastroViagemServiceTests
{
    public class DeleteTests
    {
        [Fact]
        public async Task Test_DeleteViagem_Success_WhenValidId()
        {
            // Arrange
            var viagem = new Viagem()
            {
                Id = 1,
                Origem = "AAA",
                Destino = "BBB",
                Custo = 150
            };
            var repository = new Mock<IRepositoryCrud<Viagem>>();
            repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(viagem);
            repository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
            var service = new CadastroViagemService(repository.Object);
            var viagemId = 1;

            // Act
            await service.DeleteViagem(viagemId);

            // Assert
            repository.Verify(x => x.DeleteAsync(viagemId), Times.Once);
            repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }


        // test Delete, that raises a NaoEncontradoException
        [Fact]
        public async Task Test_DeleteViagem_RaisesNaoEncontradoException_WhenInvalidId()
        {
            // Arrange
            var repository = new Mock<IRepositoryCrud<Viagem>>();
            var service = new CadastroViagemService(repository.Object);

            // Act
            async Task act() => await service.DeleteViagem(1);

            // Assert
            await Assert.ThrowsAsync<NaoEncontradoException>(act);
            repository.Verify(x => x.GetByIdAsync(1), Times.Once);
            repository.Verify(x => x.DeleteAsync(1), Times.Never);
        }

    }
}