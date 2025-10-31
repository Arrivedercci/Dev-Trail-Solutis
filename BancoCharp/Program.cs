using System;  

class Program
{
    static void Main(string[] args)
    {
        Conta conta1 = new ContaCorrente("1", "João Silva");
        Conta conta2 = new ContaCorrente("2", "Maria Santos");

        Console.WriteLine(conta1);
        Console.WriteLine(conta2);

        conta1.Depositar(500.00m);
        Console.WriteLine(conta1);
        Console.WriteLine(conta1.Saldo);

        conta1.Sacar(200.00m);
        Console.WriteLine(conta1);

        Conta conta3 = new ContaEspecial("3", "Carlos Pereira", 200.00m, 1000.00m);
        Console.WriteLine(conta3);
        conta3.Sacar(1100.00m);
        Console.WriteLine(conta3);

        Conta conta4 = new ContaPoupanca("4", "Ana Costa", 0.05m, 300.00m);
        Console.WriteLine(conta4.CalcularTarifa());
     
    
    }
}