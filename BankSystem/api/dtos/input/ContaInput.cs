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



        public Conta toConta(Cliente cliente)
        {
            return new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = NumeroConta,
                Saldo = Saldo,
                Tipo = Tipo,
                DataCriacao = DataCriacao,
                Status = Status,
                ClienteId = cliente.Id,
                Cliente = cliente
            };
        }
    }


}


