using Microsoft.EntityFrameworkCore;
using Api.Models;
using BankSystem.Data;
using BankSystem.Api.Repositories;

namespace BankSystem.Unit.Test
{
    public class GetClienteByIdAsyncTest
    {
        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetClienteByIdAsync_ReturnsCliente_WhenExists()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "22233344455",
                Nome = "Cliente Por Id",
                Contas = new List<Conta>()
            };

            using (var seed = new BankContext(options))
            {
                seed.Clientes.Add(cliente);
                await seed.SaveChangesAsync();
            }

            using (var context = new BankContext(options))
            {
                var repo = new ClienteRepository(context);
                var result = await repo.GetClienteByIdAsync(cliente.Id);

                Assert.NotNull(result);
                Assert.Equal(cliente.Id, result!.Id);
                Assert.Equal(cliente.Cpf, result.Cpf);
            }
        }

        [Fact]
        public async Task GetClienteByIdAsync_ReturnsNull_WhenNotFound()
        {
            var options = CreateNewContextOptions();

            using (var context = new BankContext(options))
            {
                var repo = new ClienteRepository(context);
                var result = await repo.GetClienteByIdAsync(Guid.NewGuid());
                Assert.Null(result);
            }
        }

    }
}