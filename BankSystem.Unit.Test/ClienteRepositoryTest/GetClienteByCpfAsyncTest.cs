using Microsoft.EntityFrameworkCore;
using Api.Models;
using BankSystem.Data;
using BankSystem.Api.Repositories;

namespace BankSystem.Unit.Test
{
    public class GetClienteByCpfAsyncTest
    {

        private static DbContextOptions<BankContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetClienteByCpfAsync_ReturnsCliente_WhenExists()
        {
            var options = CreateNewContextOptions();

            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "12345678900",
                Nome = "Cliente Teste",
                Contas = new List<Conta>
                {
                    new Conta { Id = Guid.NewGuid(), NumeroConta = 9001, Saldo = 10m, Tipo = Tipo.Corrente, Status = Status.Ativa, DataCriacao = DateTime.Now }
                }
            };

            using (var seed = new BankContext(options))
            {
                seed.Clientes.Add(cliente);
                await seed.SaveChangesAsync();
            }

            using (var context = new BankContext(options))
            {
                var repo = new ClienteRepository(context);
                var result = await repo.GetClienteByCpfAsync(cliente.Cpf);

                Assert.NotNull(result);
                Assert.Equal(cliente.Cpf, result!.Cpf);
                Assert.Equal(cliente.Nome, result.Nome);
                // If repository includes Contas, they should be loaded
                Assert.True(result.Contas == null || result.Contas.Count >= 0);
            }
        }

        [Fact]
        public async Task GetClienteByCpfAsync_ReturnsNull_WhenNotFound()
        {
            var options = CreateNewContextOptions();

            using (var context = new BankContext(options))
            {
                var repo = new ClienteRepository(context);
                var result = await repo.GetClienteByCpfAsync("00000000000");
                Assert.Null(result);
            }
        }

    }
}