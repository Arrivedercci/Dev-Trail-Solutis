using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ClienteServiceTest
{
    public class UpdateClienteAsyncTest
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public UpdateClienteAsyncTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task UpdateClienteAsync_DeveRetornarView_QuandoAtualizaComSucesso()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "44455566677",
                Nome = "Cliente Para Update",
                Contas = new List<Conta>()
            };

            _clienteRepositoryMock
                .Setup(r => r.GetClienteByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(cliente);

            _clienteRepositoryMock
                .Setup(r => r.UpdateClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _clienteService.UpdateClienteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Cpf, result!.Cpf);
            _clienteRepositoryMock.Verify(r => r.UpdateClienteAsync(It.Is<Cliente>(c => c.Id == cliente.Id)), Times.Once);
        }

        [Fact]
        public async Task UpdateClienteAsync_DeveRetornarNull_QuandoClienteNaoExiste()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(r => r.GetClienteByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Cliente?)null);

            // Act
            var result = await _clienteService.UpdateClienteAsync(1);

            // Assert
            Assert.Null(result);
            _clienteRepositoryMock.Verify(r => r.UpdateClienteAsync(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
