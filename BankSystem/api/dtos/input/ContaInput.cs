using System.ComponentModel;
using Api.Models;

namespace Api.Dtos.Input
{
    public class ContaInput
    {
        public Guid Id { get; set; }

        public int NumeroConta { get; set; }

        public decimal Saldo { get; set; }

        public Tipo Tipo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public Status Status { get; set; }

        public string CpfCliente { get; set; } = string.Empty;

        public string NomeCliente { get; set; } = string.Empty;



        public Conta toConta(ContaInput input, Cliente cliente)
        {
            return new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = input.NumeroConta,
                Saldo = input.Saldo,
                Tipo = input.Tipo,
                DataCriacao = input.DataCriacao,
                Status = input.Status,
                ClienteId = cliente.Id,
                Cliente = cliente
            };
        }
    }


}


