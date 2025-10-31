using System;

public class ContaPoupanca : Conta{

    decimal taxaJuros;
    public decimal tarifa { get; private set; }

    public ContaPoupanca(string numeroConta, string titular, decimal taxaJuros, decimal saldoInicial = 0.00m)
    : base(numeroConta, titular, saldoInicial){
        this.taxaJuros = taxaJuros;
    }
        public override decimal CalcularTarifa()
    {   
        this.tarifa = 5.00m;
        return this.tarifa;
    }
}