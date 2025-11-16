using Api.Dtos.Input;
using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class SacarAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public SacarAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task SacarAsync_DeveRetornarTrue_QuandoSaldoSuficiente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var conta = new Conta { Id = id, NumeroConta = 55555, Saldo = 200m };
            _contaRepositoryMock.Setup(r => r.GetContaByIdAsync(id)).ReturnsAsync(conta);
            _contaRepositoryMock.Setup(r => r.UpdateContaAsync(It.IsAny<Conta>())).Returns(Task.CompletedTask);

            // Act
            var ok = await _contaService.SacarAsync(id, 100m);

            // Assert
            Assert.True(ok);
            Assert.Equal(100m, conta.Saldo);
            _contaRepositoryMock.Verify(r => r.UpdateContaAsync(It.Is<Conta>(c => c.Id == id && c.Saldo == 100m)), Times.Once);
        }

        [Fact]
        public async Task SacarAsync_DeveRetornarFalse_QuandoSaldoInsuficiente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var conta = new Conta { Id = id, NumeroConta = 66666, Saldo = 50m };
            _contaRepositoryMock.Setup(r => r.GetContaByIdAsync(id)).ReturnsAsync(conta);

            // Act
            var ok = await _contaService.SacarAsync(id, 100m);

            // Assert
            Assert.False(ok);
            Assert.Equal(50m, conta.Saldo); // saldo não deve mudar
            _contaRepositoryMock.Verify(r => r.UpdateContaAsync(It.IsAny<Conta>()), Times.Never);
        }

        [Fact]
        public async Task SacarAsync_DeveRetornarFalse_QuandoContaNaoExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            _contaRepositoryMock.Setup(r => r.GetContaByIdAsync(id)).ReturnsAsync((Conta?)null);

            // Act
            var ok = await _contaService.SacarAsync(id, 10m);

            // Assert
            Assert.False(ok);
            _contaRepositoryMock.Verify(r => r.UpdateContaAsync(It.IsAny<Conta>()), Times.Never);
        }
    }
}
