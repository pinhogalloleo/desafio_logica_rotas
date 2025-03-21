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
    public class UpdateTests
    {
        [Fact]
        public async Task Test_UpdateViagem_Success_WhenValidViagem()
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
            repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(viagem));
            repository.Setup(x => x.UpdateAsync(It.IsAny<Viagem>())).Returns(Task.FromResult(viagem));
            var service = new CadastroViagemService(repository.Object);

            // Act
            await service.UpdateViagem(viagem);

            // Assert
            repository.Verify(x => x.UpdateAsync(viagem), Times.Once);
            repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        // test exception when viagem is null
        [Fact]
        public async Task Test_UpdateViagem_ThrowsArgumentNullException_WhenViagemIsNull()
        {
            // Arrange
            var repository = new Mock<IRepositoryCrud<Viagem>>();
            var service = new CadastroViagemService(repository.Object);

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateViagem(null));

            // Assert
            Assert.Contains("objeto viagem não pode ser nulo", exception.Message.ToLowerInvariant());
        }

        [Fact] // test NaoEncontradoException when viagem not found
        public async Task Test_UpdateViagem_ThrowsNaoEncontradoException_WhenViagemNotFound()
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
            var service = new CadastroViagemService(repository.Object);

            // Act
            var exception = await Assert.ThrowsAsync<NaoEncontradoException>(() => service.UpdateViagem(viagem));

            // Assert
            Assert.Equal("Viagem não encontrada", exception.Message);
        }

        [Fact] // test invalid viagem
        public async Task Test_UpdateViagem_ThrowsValidacaoException_WhenInvalidViagem()
        {
            // Arrange
            var viagem = new Viagem()
            {
                Id = 1,
                Origem = "AAA",
                Destino = "BBB",
                Custo = -1
            };
            var repository = new Mock<IRepositoryCrud<Viagem>>();
            repository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(viagem));
            var service = new CadastroViagemService(repository.Object);

            // Act
            var exception = await Assert.ThrowsAsync<ValidacaoException>(() => service.UpdateViagem(viagem));

            // Assert
            Assert.Contains("custo", exception.Message.ToLowerInvariant());
        }

    }
}