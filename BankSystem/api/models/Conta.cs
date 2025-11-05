using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Conta
{
    [Key]
    public Guid id { get; set; }

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

}