public class Betano : IObserver
{
    public string Nome { get; set; } = "Betano";

    public void ReceberNotificacao(object? sender, GolEventArgs e)
    {

        Console.WriteLine($"   [{Nome}]: Deu Greeen!!!!! '{e.jogador}' do '{e.time}' marcou um gol contra o '{e.adversario}'!");
    }
}