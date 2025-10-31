using System;

public class ContaEspecial : Conta 
{
    decimal limiteEspecial;
    public decimal tarifa   { get; private set; }

    public ContaEspecial(string numeroConta, string titular, decimal limiteEspecial, decimal saldoInicial = 0.00m)
    : base(numeroConta, titular, saldoInicial)
    {
        this.limiteEspecial = limiteEspecial;
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0 || valor > Saldo + limiteEspecial)
        {
            return false;
        }

        _saldo -= valor;
        return true;

    }

        
        
    public override decimal CalcularTarifa()
    {
        this.tarifa = this.limiteEspecial * 0.01m;
        return this.tarifa;
    }
}