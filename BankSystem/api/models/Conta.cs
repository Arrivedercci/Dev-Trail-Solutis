using System;

public class Conta
{
public int NumeroConta { get; private set; }
    public string Titular { get; private set; }
    public decimal Saldo { get; private set; }
    public string Tipo { get; private set; }

    public Conta(int numeroConta, string titular, string tipo, decimal saldoInicial = 0m)
    {
        NumeroConta = numeroConta;
        Titular = titular;
        Tipo = tipo;
        Saldo = saldoInicial;
    }



    public override string ToString()
    {
        return $"Conta: [{NumeroConta},{Titular},{Tipo}, Saldo:{Saldo}]";
    }
}


