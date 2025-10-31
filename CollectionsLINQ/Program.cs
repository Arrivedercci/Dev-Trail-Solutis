using System;

class Program
{
    static void Main(string[] args)
    {

        ContaCorrente conta1 = new ContaCorrente(1, "Alice Silva", "Corrente", 1500.00m);
        ContaCorrente conta2 = new ContaCorrente(2, "Bruno Souza", "Poupança", 2500.00m);
        ContaCorrente conta3 = new ContaCorrente(3, "Carlos Oliveira", "Corrente", -3000.00m);
        ContaCorrente conta4 = new ContaCorrente(4, "Diana Pereira", "Poupança", 4000.00m);
        ContaCorrente conta5 = new ContaCorrente(5, "Eduardo Lima", "Especial", 5000.00m);

        Dictionary<int, ContaCorrente> contas = new Dictionary<int, ContaCorrente>();


        contas.Add(conta1.NumeroConta, conta1);
        contas.Add(conta2.NumeroConta, conta2);
        contas.Add(conta3.NumeroConta, conta3);
        contas.Add(conta4.NumeroConta, conta4);
        contas.Add(conta5.NumeroConta, conta5);


        var contaDesejada = (from conta in contas
                             where conta.Value.NumeroConta == 4
                             select conta.Value).ToList();

        var contaDesejadaMethod = contas.Where(c => c.Value.NumeroConta == 4).ToList();

        var contasNegativadas = (from conta in contas
                                 where conta.Value.Saldo < 0
                                 select conta.Value).ToList();

        var contasNegativadasMethod = contas.Where(c => c.Value.Saldo < 0).ToList();


        Console.WriteLine("Conta desejada: " + contaDesejada[0].ToString());
        Console.WriteLine("Contas negativadas: " + contasNegativadas[0].ToString());

        var contasAgrupadas = from conta in contas
                              group conta.Value by conta.Value.Tipo into grupo
                              select grupo;

        foreach (var grupo in contasAgrupadas)
        {
            Console.WriteLine($"Tipo de Conta: {grupo.Key}");
            Console.WriteLine("Numero de Contas: " + grupo.Count());
        }


        var contasOrdenadas = from conta in contas
                              orderby conta.Value.Saldo descending, conta.Value.Titular descending
                              select conta.Value;

        Console.WriteLine("Contas ordenadas por saldo (decrescente):");
        foreach (var conta in contasOrdenadas)
        {
            Console.WriteLine(conta.ToString());
        }

        var contasSelecionadas = from conta in contas
                                 select new { conta.Value.Titular, conta.Value.Saldo };

        foreach (var conta in contasSelecionadas)
        {
            Console.WriteLine($"Titular: {conta.Titular}, Saldo: {conta.Saldo}");
        }


        var contasConsultadas = from conta in contas
                                group conta.Value by conta.Value.Tipo into grupo
                                select new
                                {
                                    TipoConta = grupo.Key,
                                    Contas = grupo.OrderByDescending(c => c.Saldo)
                                    .ThenByDescending(c => c.Titular).Select(c => new { c.Titular, c.Saldo })

                                };

        foreach (var grupo in contasConsultadas)
        {
            Console.WriteLine($"Tipo de Conta: {grupo.TipoConta}");
            foreach (var conta in grupo.Contas)
            {
                Console.WriteLine($"Titular: {conta.Titular}, Saldo: {conta.Saldo}");
            }

        }
    }
}