using System;

public class XucoScore : IObserver
{
    public string Nome { get; set; } = "XucoScore";

    public void ReceberNotificacao(object? sender, GolEventArgs e)
    {

        Console.WriteLine($"   [{Nome}]: Golaçoooo!!!! de '{e.jogador}' do '{e.time}' marcou um gol contra o '{e.adversario}'!");
    }
}