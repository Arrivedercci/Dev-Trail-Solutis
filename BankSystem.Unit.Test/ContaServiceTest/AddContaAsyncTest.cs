using Api.Dtos.Input;
using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ContaServiceTest
{
    public class AddContaAsyncTest
    {

        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ContaService _contaService;

        public AddContaAsyncTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _contaService = new ContaService(_contaRepositoryMock.Object, _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task AddContaAsync_DeveCriarClienteQuandoNaoExiste_EAdicionarConta()
        {
            // Arrange
            var input = new ContaInput
            {
                NumeroConta = 22222,
                Saldo = 50m,
                Tipo = Tipo.Corrente,
                Status = Status.Ativa,
                CpfCliente = "99999999999",
                NomeCliente = "Cliente Novo"
            };

            // cliente não existe
            _clienteRepositoryMock
                .Setup(r => r.GetClienteByCpfAsync(input.CpfCliente))
                .ReturnsAsync((Cliente?)null);

            _clienteRepositoryMock
                .Setup(r => r.AddClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            _contaRepositoryMock
                .Setup(r => r.AddContaAsync(It.IsAny<Conta>()))
                .Returns(Task.CompletedTask);

            _clienteRepositoryMock
                .Setup(r => r.UpdateClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _contaService.AddContaAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.NumeroConta, result!.NumeroConta);

            _clienteRepositoryMock.Verify(r => r.GetClienteByCpfAsync(input.CpfCliente), Times.Once);
            _clienteRepositoryMock.Verify(r => r.AddClienteAsync(It.Is<Cliente>(c => c.Cpf == input.CpfCliente && c.Nome == input.NomeCliente)), Times.Once);
            _contaRepositoryMock.Verify(r => r.AddContaAsync(It.IsAny<Conta>()), Times.Once);
            _clienteRepositoryMock.Verify(r => r.UpdateClienteAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task AddContaAsync_DeveUsarClienteExistente_EAdicionarConta()
        {
            // Arrange
            var existingCliente = new Cliente { Id = Guid.NewGuid(), Cpf = "77777777777", Nome = "Cliente Existente", Contas = new List<Conta>() };
            var input = new ContaInput
            {
                NumeroConta = 33333,
                Saldo = 100m,
                Tipo = Tipo.Poupanca,
                Status = Status.Ativa,
                CpfCliente = existingCliente.Cpf,
                NomeCliente = existingCliente.Nome
            };

            _clienteRepositoryMock
                .Setup(r => r.GetClienteByCpfAsync(existingCliente.Cpf))
                .ReturnsAsync(existingCliente);

            _contaRepositoryMock
                .Setup(r => r.AddContaAsync(It.IsAny<Conta>()))
                .Returns(Task.CompletedTask);

            _clienteRepositoryMock
                .Setup(r => r.UpdateClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _contaService.AddContaAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.NumeroConta, result!.NumeroConta);
            _clienteRepositoryMock.Verify(r => r.AddClienteAsync(It.IsAny<Cliente>()), Times.Never);
            _contaRepositoryMock.Verify(r => r.AddContaAsync(It.IsAny<Conta>()), Times.Once);
            _clienteRepositoryMock.Verify(r => r.UpdateClienteAsync(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
