using System;

public class Xuco365 : IObserver
{
    public string Nome { get; set; } = "365Xuco";

    public void ReceberNotificacao(object? sender, GolEventArgs e)
    {

        Console.WriteLine($"   [{Nome}]: Golaçoooo!!!! de '{e.jogador}' do '{e.time}' marcou um gol contra o '{e.adversario}'!");
    }
}