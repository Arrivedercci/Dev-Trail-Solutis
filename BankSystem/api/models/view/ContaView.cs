namespace Api.Models.View
{
    public class ContaView
    {
        public Guid NumeroConta { get; init; }
        public string Titular { get; init; } = string.Empty;
        public decimal Saldo { get; init; }
        public string Tipo { get; init; } = string.Empty;

        public override string ToString() =>
            $"Conta: [{NumeroConta},{Titular},{Tipo}, Saldo:{Saldo}]";

    }
}