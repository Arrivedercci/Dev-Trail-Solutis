using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class DeleteContaAsyncTest
    {

        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task DeleteContaAsync_RemovesConta_WhenExists()
        {
            var options = CreateNewContextOptions();

            var conta = new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = 6001,
                Saldo = 300m,
                Tipo = Tipo.Corrente,
                Status = Status.Ativa,
                DataCriacao = DateTime.Now,
                Cliente = null,
                ClienteId = Guid.NewGuid()
            };

            using (var seed = new BankContext(options))
            {
                seed.Contas.Add(conta);
                await seed.SaveChangesAsync();
            }

            using (var context = new BankContext(options))
            {
                var repo = new ContaRepository(context);
                var result = await repo.DeleteContaAsync(conta.Id);

                Assert.True(result);
            }

            using (var verify = new BankContext(options))
            {
                var exists = await verify.Contas.AnyAsync(c => c.Id == conta.Id);
                Assert.False(exists);
            }
        }

        [Fact]
        public async Task DeleteContaAsync_ReturnsFalse_WhenNotFound()
        {
            var options = CreateNewContextOptions();

            using (var context = new BankContext(options))
            {
                var repo = new ContaRepository(context);
                var result = await repo.DeleteContaAsync(Guid.NewGuid());

                Assert.False(result);
            }


        }
    }
}
