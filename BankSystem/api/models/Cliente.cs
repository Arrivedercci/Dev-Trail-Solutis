using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

        public List<Conta> Contas { get; set; } = [];

        public Cliente()
        {

        }

        public static async Task<Cliente?> GetClienteByCpf(BankContext context, string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return null;

            return await context.Clientes.Include(c => c.Contas).FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public static async Task<Cliente?> GetClienteById(BankContext context, Guid id)
        {
            if (id == Guid.Empty) return null;

            return await context.Clientes.Include(c => c.Contas).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}