using Microsoft.EntityFrameworkCore;
using Api.Models;
using BankSystem.Data;
using BankSystem.Api.Repositories;

namespace BankSystem.Unit.Test
{
    public class AddClienteAsyncTest
    {
        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddClienteAsync_PersistsCliente()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "33344455566",
                Nome = "Novo Cliente",
                Contas = new List<Conta>()
            };

            using (var context = new BankContext(options))
            {
                var repo = new ClienteRepository(context);
                await repo.AddClienteAsync(cliente);
            }

            using (var verify = new BankContext(options))
            {
                var saved = await verify.Clientes.FirstOrDefaultAsync(c => c.Cpf == cliente.Cpf);
                Assert.NotNull(saved);
                Assert.Equal(cliente.Nome, saved!.Nome);
            }
        }

    }
}