using System;

public class ContaCorrente : Conta
{
    public decimal tarifa { get; private set; }
    public ContaCorrente(string numeroConta, string titular, decimal saldoInicial = 0.00m)
    : base(numeroConta, titular, saldoInicial)
    {

    }

    public override decimal CalcularTarifa()
    {  
        this.tarifa = 20.00m;
        return this.tarifa;
    }
}
