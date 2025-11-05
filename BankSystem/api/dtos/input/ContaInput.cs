using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Input
{
    public class ContaInput
    {
        [Required]
        public Guid NumeroConta { get; set; }

        [Required]
        [StringLength(100)]
        public required string Titular { get; set; }

        [Required]
        [RegularExpression("^(Corrente|Poupanca|Especial)$", ErrorMessage = "Tipo inválido.")]
        public required string Tipo { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SaldoInicial { get; set; } = 0m;
    }
}


