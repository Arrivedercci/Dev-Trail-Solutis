using System;

public class Program
{
    public static void Main(string[] args)
    {
        Conta conta = new Conta(12345, "João Silva", 1000.00m);

        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 500.00m, "Crédito", "Pix", new DateTime(2025, 10, 29, 09, 30, 17)));
        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 200.00m, "Débito", "Pagamento de conta", new DateTime(2025, 10, 29, 16, 30, 04)));
        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 150.00m, "Crédito", "Transferência recebida", new DateTime(2025, 10, 29, 07, 36, 53)));
        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 300.00m, "Débito", "Saque em caixa eletrônico", new DateTime(2025, 10, 29, 00, 00, 00)));
        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 100.00m, "Crédito", "Rendimento de investimento", new DateTime(2025, 10, 29, 13, 30, 00)));
        conta.AdicionarTransacao(new Transacao(Guid.NewGuid(), conta.NumeroConta, 50.00m, "Débito", "Compra no supermercado", new DateTime(2025, 10, 29, 21, 58, 37)));


        var extrato = (from transacao in conta.Transacoes
                       orderby transacao.Data
                       select transacao);


        foreach (var t in extrato)
        {
            Console.WriteLine(t);
        }


    }
}
