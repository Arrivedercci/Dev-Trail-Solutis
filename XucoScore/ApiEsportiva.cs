using System;
public  class ApiEsportiva
{
    public event EventHandler<GolEventArgs>? NovoGolMarcado;

    public List<IObserver> Observadores { get; set; } = new List<IObserver>();

    public void RegistrarObservador(IObserver observador)
    {
        Observadores.Add(observador);
    }

    public void RemoverObservador(IObserver observador)
    {
        Observadores.Remove(observador);
    }

    public void NotificarObservadores(GolEventArgs e)
    {
        MarcarGol(e.time, e.adversario, e.jogador);
        foreach (var observador in Observadores)
        {
            observador.ReceberNotificacao(this, e);
        }
    }

    public void MarcarGol(string time, string adversario, string jogador)
    {
        Console.WriteLine($"\n Novo gol de '{time}' contra '{adversario}' marcado por '{jogador}'");

        OnNovoGolMarcado(new GolEventArgs { time = time, adversario = adversario, jogador = jogador });
    }

    protected virtual void OnNovoGolMarcado(GolEventArgs e)
    {

        NovoGolMarcado?.Invoke(this, e);
    }

}