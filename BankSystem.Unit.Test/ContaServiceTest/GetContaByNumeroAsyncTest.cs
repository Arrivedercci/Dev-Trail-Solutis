using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class GetContaByNumeroAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public GetContaByNumeroAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task GetContaByNumeroAsync_DeveRetornarConta_QuandoContaExiste()
        {
            // Arrange
            var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "12345678900", Nome = "João Silva" };
            var conta = new Conta { Id = Guid.NewGuid(), NumeroConta = 12345, Saldo = 1000m, Cliente = cliente };

            _contaRepositoryMock
                .Setup(repo => repo.GetContaByNumeroAsync(12345))
                .ReturnsAsync(conta);
            // Act
            var result = await _contaService.GetContaByNumeroAsync(12345);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(12345, result!.NumeroConta);
            Assert.NotNull(result.Cliente);
            Assert.Equal(cliente.Cpf, result.Cliente!.Cpf);
        }
    }
}
