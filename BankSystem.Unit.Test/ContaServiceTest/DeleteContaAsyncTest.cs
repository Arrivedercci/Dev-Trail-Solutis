using Api.Dtos.Input;
using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class DeleteContaAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public DeleteContaAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteContaAsync_DeveRetornarTrue_QuandoExcluiComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _contaRepositoryMock.Setup(r => r.DeleteContaAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _contaService.DeleteContaAsync(id);

            // Assert
            Assert.True(result);
            _contaRepositoryMock.Verify(r => r.DeleteContaAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteContaAsync_DeveRetornarFalse_QuandoNaoExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            _contaRepositoryMock.Setup(r => r.DeleteContaAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _contaService.DeleteContaAsync(id);

            // Assert
            Assert.False(result);
            _contaRepositoryMock.Verify(r => r.DeleteContaAsync(id), Times.Once);
        }
    }
}
