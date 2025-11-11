using System;

public class SofaScore : IObserver
{
    public string Nome { get; set; } = "SofaScore";

    public void ReceberNotificacao(object? sender, GolEventArgs e)
    {

        Console.WriteLine($"   [{Nome}]: Gol de '{e.jogador}' do '{e.time}' marcou um gol contra o '{e.adversario}'!");
    }
}