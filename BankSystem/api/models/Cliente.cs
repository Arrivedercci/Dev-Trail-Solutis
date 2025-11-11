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
    }
}