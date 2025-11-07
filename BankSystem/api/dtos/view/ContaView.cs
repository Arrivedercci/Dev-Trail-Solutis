using System.ComponentModel;
using Api.Models;
using Microsoft.Identity.Client;

namespace Api.Dtos.View
{
    public class ContaView
    {

        public Guid Id { get; set; }

        public int NumeroConta { get; set; }

        public decimal Saldo { get; set; }

        public Tipo Tipo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public Status Status { get; set; }

        public Guid ClienteId { get; set; }

        public ClienteView? Cliente { get; set; }


        public static ContaView toContaView(Conta conta)
        {
            return new ContaView
            {
                Id = conta.Id,
                NumeroConta = conta.NumeroConta,
                Saldo = conta.Saldo,
                Tipo = conta.Tipo,
                DataCriacao = conta.DataCriacao,
                Status = conta.Status,
                ClienteId = conta.ClienteId
            };
        }

        public static ContaView toContaView(Conta conta, ClienteView cliente)
        {
            return new ContaView
            {
                Id = conta.Id,
                NumeroConta = conta.NumeroConta,
                Saldo = conta.Saldo,
                Tipo = conta.Tipo,
                DataCriacao = conta.DataCriacao,
                Status = conta.Status,
                ClienteId = conta.ClienteId,
                Cliente = cliente
            };
        }

    }
}