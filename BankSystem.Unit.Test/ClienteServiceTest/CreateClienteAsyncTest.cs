using Api.Models;
using Api.Services;
using BankSystem.Api.Repositories;
using Moq;

namespace BankSystem.Unit.Test.ClienteServiceTest
{
    public class CreateClienteAsyncTest
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public CreateClienteAsyncTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateClienteAsync_DeveCriarE_RetornarClienteView()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "11122233344",
                Nome = "Novo Cliente",
                Contas = new List<Conta>()
            };

            _clienteRepositoryMock
                .Setup(r => r.AddClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _clienteService.CreateClienteAsync(cliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Cpf, result!.Cpf);
            Assert.Equal(cliente.Nome, result.Nome);
            _clienteRepositoryMock.Verify(r => r.AddClienteAsync(It.Is<Cliente>(c => c.Cpf == cliente.Cpf && c.Nome == cliente.Nome)), Times.Once);
        }



    }
}
