using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class GetAllContasAsyncTest
    {

        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [Fact]
        public async Task GetAllContasAsync_ReturnsAllContas()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "33344455566", Nome = "Cliente3" };
            var contas = new List<Conta>
            {
                new Conta { Id = Guid.NewGuid(), NumeroConta = 3001, Saldo = 100m, Tipo = Tipo.Corrente, Status = Status.Ativa, DataCriacao = DateTime.Now, Cliente = cliente, ClienteId = cliente.Id },
                new Conta { Id = Guid.NewGuid(), NumeroConta = 3002, Saldo = 200m, Tipo = Tipo.Poupanca, Status = Status.Ativa, DataCriacao = DateTime.Now, Cliente = cliente, ClienteId = cliente.Id }
            };

            using (var seed = new BankContext(options))
            {
                seed.Clientes.Add(cliente);
                seed.Contas.AddRange(contas);
                await seed.SaveChangesAsync();
            }

            using (var context = new BankContext(options))
            {
                var repo = new ContaRepository(context);
                var result = (await repo.GetAllContasAsync()).ToList();

                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.Contains(result, c => c!.NumeroConta == 3001);
                Assert.All(result, c => Assert.NotNull(c!.Cliente)); // Include should have loaded Cliente
            }
        }

    }
}
