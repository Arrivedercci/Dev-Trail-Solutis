using System;

public class ContaCorrente
{
    public int NumeroConta { get; private set; }
    public string Titular { get; private set; }
    public decimal Saldo { get; private set; }
    public string Tipo { get; private set; }

    public ContaCorrente(int numeroConta, string titular, string tipo, decimal saldoInicial = 0m)
    {
        NumeroConta = numeroConta;
        Titular = titular;
        Tipo = tipo;
        Saldo = saldoInicial;
    }



    public override string ToString()
    {
        return $"ContaCorrente [{NumeroConta},{Titular},{Tipo}, Saldo:{Saldo}]";
    }
}

