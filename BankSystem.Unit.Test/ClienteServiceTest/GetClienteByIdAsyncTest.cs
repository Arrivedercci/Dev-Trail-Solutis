using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ClienteServiceTest
{
    public class GetClienteByIdAsyncTest
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public GetClienteByIdAsyncTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task GetClienteByIdAsync_DeveRetornarView_QuandoClienteExiste()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "33344455566",
                Nome = "Cliente Por Id",
                Contas = new List<Conta>()
            };

            // ClienteService.GetClienteByIdAsync(int id) chama o repositório com um Guid (it uses Guid.NewGuid() inside),
            // então aceitaremos qualquer Guid na configuração do mock.
            _clienteRepositoryMock
                .Setup(r => r.GetClienteByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(cliente);

            // Act
            var result = await _clienteService.GetClienteByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Cpf, result!.Cpf);
            Assert.Equal(cliente.Nome, result.Nome);
        }

        [Fact]
        public async Task GetClienteByIdAsync_DeveRetornarNull_QuandoNaoExiste()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(r => r.GetClienteByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Cliente?)null);

            // Act
            var result = await _clienteService.GetClienteByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

    }
}
