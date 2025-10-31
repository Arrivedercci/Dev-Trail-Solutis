using System;

public class Conta
{
    public int NumeroConta { get; set; }
    public string Titular { get; set; }
    public decimal Saldo { get; set; } 

    public List<Transacao> Transacoes { get; set; } = new List<Transacao>();


    public Conta(int numeroConta, string titular, decimal saldo)
    {
        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldo;
    }


    public void AdicionarTransacao(Transacao transacao)
    {
        if (transacao.Tipo == "Crédito")
        {
            Saldo += transacao.Valor;
        }
        else if (transacao.Tipo == "Débito")
        {
            Saldo -= transacao.Valor;
        }
        
        Transacoes.Add(transacao);
    }
    


}
