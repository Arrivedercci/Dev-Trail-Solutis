using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class GetContaByIdAsyncTest
    {
        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetContaByIdAsync_ReturnsConta_WhenExists()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "22233344455", Nome = "Cliente2" };
            var conta = new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = 2002,
                Saldo = 750m,
                Tipo = Tipo.Poupanca,
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
                var result = await repo.GetContaByIdAsync(conta.Id);

                Assert.NotNull(result);
                Assert.Equal(conta.Id, result!.Id);
                Assert.Equal(2002, result.NumeroConta);
            }
        }

    }
}
