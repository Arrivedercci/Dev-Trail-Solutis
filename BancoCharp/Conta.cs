using System;

public abstract class Conta : ITransacionavel
{
    public string NumeroConta { get; private set; }
    public string Titular { get; private set; }
    protected decimal _saldo { get; set; }
    public decimal Saldo  => _saldo;




    public Conta(string numeroConta, string titular, decimal saldoInicial = 0.00m)
    {
        this.NumeroConta = numeroConta;
        this.Titular = titular;
        this._saldo = saldoInicial;
    }

    public virtual Boolean Sacar(decimal valor)
    {
        if (valor <= 0 || valor > Saldo)
        {
            return false;
        }
        _saldo -= valor;
        return true;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("O valor do depósito deve ser maior que zero.");
        }
        _saldo += valor;
    }

    public abstract decimal CalcularTarifa();


    public override string ToString()
    {
        return $"Conta {NumeroConta}: {Titular}, Saldo: {Saldo}";
    }


}


