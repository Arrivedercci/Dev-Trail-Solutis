using Api.Dtos.Input;
using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class DepositarAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public DepositarAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }
        [Fact]
        public async Task DepositarAsync_DeveRetornarTrue_QuandoContaExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            var conta = new Conta { Id = id, NumeroConta = 44444, Saldo = 100m };
            _contaRepositoryMock.Setup(r => r.GetContaByIdAsync(id)).ReturnsAsync(conta);
            _contaRepositoryMock.Setup(r => r.UpdateContaAsync(It.IsAny<Conta>())).Returns(Task.CompletedTask);

            // Act
            var ok = await _contaService.DepositarAsync(id, 50m);

            // Assert
            Assert.True(ok);
            Assert.Equal(150m, conta.Saldo);
            _contaRepositoryMock.Verify(r => r.UpdateContaAsync(It.Is<Conta>(c => c.Id == id && c.Saldo == 150m)), Times.Once);
        }

        [Fact]
        public async Task DepositarAsync_DeveRetornarFalse_QuandoContaNaoExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            _contaRepositoryMock.Setup(r => r.GetContaByIdAsync(id)).ReturnsAsync((Conta?)null);

            // Act
            var ok = await _contaService.DepositarAsync(id, 50m);

            // Assert
            Assert.False(ok);
            _contaRepositoryMock.Verify(r => r.UpdateContaAsync(It.IsAny<Conta>()), Times.Never);
        }
    }
}
