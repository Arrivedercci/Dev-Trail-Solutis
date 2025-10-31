using System;

class Program
{
    static void Main(string[] args)
    {

        ContaCorrente conta1 = new ContaCorrente(1, "Alice Silva", "Corrente", 1500.00m);
        ContaCorrente conta2 = new ContaCorrente(2, "Bruno Souza", "Poupança", 2500.00m);
        ContaCorrente conta3 = new ContaCorrente(3, "Carlos Oliveira", "Corrente", 3000.00m);
        ContaCorrente conta4 = new ContaCorrente(4, "Diana Pereira", "Poupança", 4000.00m);
        ContaCorrente conta5 = new ContaCorrente(5, "Eduardo Lima", "Especial", 5000.00m);

        Dictionary<int, ContaCorrente> contas = new Dictionary<int, ContaCorrente>();


        contas.Add(conta1.NumeroConta, conta1);
        contas.Add(conta2.NumeroConta, conta2);
        contas.Add(conta3.NumeroConta, conta3);
        contas.Add(conta4.NumeroConta, conta4);
        contas.Add(conta5.NumeroConta, conta5);


        contas.TryGetValue(3, out ContaCorrente? conta);
        if (conta != null)
        {
            Console.WriteLine(conta.ToString());
        }
        else
        {
            Console.WriteLine("Conta não encontrada.");
        }


        Console.WriteLine("\nTodas as contas:");
        foreach (var kvp in contas)
        {
            Console.WriteLine(kvp.Value.ToString());
        }


        contas.Remove(2);
        Console.WriteLine("\nApós remover a conta 2:");
        foreach (var kvp in contas)
        {
            Console.WriteLine(kvp.Value.ToString());
        }

        Console.WriteLine(contas.ContainsKey(3));
    }
}