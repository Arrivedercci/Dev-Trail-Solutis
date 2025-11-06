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
    }
}


