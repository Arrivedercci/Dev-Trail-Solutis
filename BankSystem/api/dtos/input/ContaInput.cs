using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Models;

namespace Api.Dtos.Input
{
    public class ContaInput
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public int NumeroConta { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        [Required]
        public Tipo Tipo { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Required]
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


