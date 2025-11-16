using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;


namespace BankSystem.Unit.Test.ClienteServiceTest
{
    public class GetClienteByCpfAsyncTest
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public GetClienteByCpfAsyncTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task GetClienteByCpfAsync_DeveRetornarView_QuandoClienteExiste()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "22233344455",
                Nome = "Cliente Existente",
                Contas = new List<Conta>
                {
                    new Conta { Id = Guid.NewGuid(), NumeroConta = 10101, Saldo = 10m }
                }
            };

            _clienteRepositoryMock
                .Setup(r => r.GetClienteByCpfAsync(cliente.Cpf))
                .ReturnsAsync(cliente);

            // Act
            var result = await _clienteService.GetClienteByCpfAsync(cliente.Cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Cpf, result!.Cpf);
            Assert.Equal(cliente.Nome, result.Nome);
            Assert.NotNull(result.Contas);
            Assert.Single(result.Contas);
        }

        [Fact]
        public async Task GetClienteByCpfAsync_DeveRetornarNull_QuandoNaoExiste()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(r => r.GetClienteByCpfAsync(It.IsAny<string>()))
                .ReturnsAsync((Cliente?)null);

            // Act
            var result = await _clienteService.GetClienteByCpfAsync("inexistente");

            // Assert
            Assert.Null(result);
        }


    }
}
