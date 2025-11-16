using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class AddContaAsyncTest
    {

        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddContaAsync_PersistsConta()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "44455566677", Nome = "Cliente4" };
            var conta = new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = 4001,
                Saldo = 10m,
                Tipo = Tipo.Corrente,
                Status = Status.Ativa,
                DataCriacao = DateTime.Now,
                Cliente = cliente,
                ClienteId = cliente.Id
            };

            using (var context = new BankContext(options))
            {
                var repo = new ContaRepository(context);
                // ensure cliente exists in DB so FK is valid
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();

                await repo.AddContaAsync(conta);
            }

            using (var verify = new BankContext(options))
            {
                var saved = await verify.Contas.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.NumeroConta == 4001);
                Assert.NotNull(saved);
                Assert.Equal(10m, saved!.Saldo);
                Assert.NotNull(saved.Cliente);
                Assert.Equal(cliente.Cpf, saved.Cliente!.Cpf);
            }
        }


    }
}
