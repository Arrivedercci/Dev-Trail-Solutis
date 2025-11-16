using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class UpdateContaAsyncTest
    {
        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task UpdateContaAsync_UpdatesPersistedConta()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "55566677788", Nome = "Cliente5" };
            var conta = new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = 5001,
                Saldo = 20m,
                Tipo = Tipo.Corrente,
                Status = Status.Ativa,
                DataCriacao = DateTime.Now,
                Cliente = cliente,
                ClienteId = cliente.Id
            };

            using (var seed = new BankContext(options))
            {
                seed.Clientes.Add(cliente);
                seed.Contas.Add(conta);
                await seed.SaveChangesAsync();
            }

            using (var context = new BankContext(options))
            {
                var repo = new ContaRepository(context);
                var loaded = await context.Contas.FirstAsync(c => c.NumeroConta == 5001);
                loaded.Saldo = 120m;

                await repo.UpdateContaAsync(loaded);
            }

            using (var verify = new BankContext(options))
            {
                var updated = await verify.Contas.FirstOrDefaultAsync(c => c.NumeroConta == 5001);
                Assert.NotNull(updated);
                Assert.Equal(120m, updated!.Saldo);
            }
        }

    }
}
