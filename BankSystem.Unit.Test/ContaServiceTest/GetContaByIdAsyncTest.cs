using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class GetContaByIdAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public GetContaByIdAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task GetContaByIdAsync_DeveRetornarConta_QuandoContaExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            var conta = new Conta { Id = id, NumeroConta = 11111, Saldo = 500m };
            _contaRepositoryMock
                .Setup(repo => repo.GetContaByIdAsync(id))
                .ReturnsAsync(conta);

            // Act
            var result = await _contaService.GetContaByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(11111, result!.NumeroConta);
            Assert.Equal(conta.Id, result.Id);
        }
    }
}
