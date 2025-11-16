using Api.Models;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace BankSystem.Unit.Test.ContaRepositoryTest
{
    public class GetContaByNumeroAsyncTest
    {
            private static DbContextOptions<BankContext> CreateNewContextOptions()
            {
                return new DbContextOptionsBuilder<BankContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            }

            [Fact]
            public async Task GetContaByNumeroAsync_ReturnsConta_WhenExists()
            {
                var options = CreateNewContextOptions();

                // seed
                var cliente = new Cliente { Id = Guid.NewGuid(), Cpf = "12345678900", Nome = "Teste" };
                var conta = new Conta
                {
                    Id = Guid.NewGuid(),
                    NumeroConta = 1001,
                    Saldo = 500m,
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

                // act / assert
                using (var context = new BankContext(options))
                {
                    var repo = new ContaRepository(context);
                    var result = await repo.GetContaByNumeroAsync(1001);

                    Assert.NotNull(result);
                    Assert.Equal(1001, result!.NumeroConta);
                    Assert.NotNull(result.Cliente);
                    Assert.Equal(cliente.Cpf, result.Cliente!.Cpf);
                }
            }
        }
}
