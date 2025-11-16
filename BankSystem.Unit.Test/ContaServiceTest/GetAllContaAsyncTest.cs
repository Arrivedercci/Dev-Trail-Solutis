using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class GetAllContaAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public GetAllContaAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }


        [Fact]
        public async Task GetAllContasAsync_DeveRetornarLista_QuandoContasExistem()
        {
            // Arrange
            var contas = new List<Conta>
            {
                new Conta { Id = Guid.NewGuid(), NumeroConta = 12345, Saldo = 1000m },
                new Conta { Id = Guid.NewGuid(), NumeroConta = 67890, Saldo = 2000m }
            };

            _contaRepositoryMock
                .Setup(repo => repo.GetAllContasAsync())
                .ReturnsAsync(contas);

            // Act
            var result = await _contaService.GetAllContasAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.NumeroConta == 12345);
        }

        [Fact]
        public async Task GetAllContasAsync_DeveRetornarListaVazia_QuandoNenhumaContaExiste()
        {
            // Arrange
            _contaRepositoryMock
                .Setup(repo => repo.GetAllContasAsync())
                .ReturnsAsync(new List<Conta>());
            // Act
            var result = await _contaService.GetAllContasAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllContasAsync_DeveRetornarUmaExecao_QuandoObjetoForNulo()
        {
            // Arrange
            _contaRepositoryMock
                .Setup(repo => repo.GetAllContasAsync())
                .ReturnsAsync((List<Conta>)null);
            // Act
            var result = await _contaService.GetAllContasAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
