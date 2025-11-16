using Microsoft.EntityFrameworkCore;
using Api.Models;
using BankSystem.Data;
using BankSystem.Api.Repositories;

namespace BankSystem.Unit.Test
{
    public class UpdateClienteAsyncTest
    {
        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task UpdateClienteAsync_UpdatesPersistedCliente()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "44455566677",
                Nome = "Cliente Para Update",
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
                var toUpdate = await context.Clientes.FirstAsync(c => c.Cpf == cliente.Cpf);
                toUpdate.Nome = "Nome Atualizado";

                await repo.UpdateClienteAsync(toUpdate);
            }

            using (var verify = new BankContext(options))
            {
                var updated = await verify.Clientes.FirstOrDefaultAsync(c => c.Cpf == cliente.Cpf);
                Assert.NotNull(updated);
                Assert.Equal("Nome Atualizado", updated!.Nome);
            }
        }
    }
}