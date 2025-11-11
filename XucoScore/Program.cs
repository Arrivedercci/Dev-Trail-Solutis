using System;
public class Program
{
    public static void Main()
    {

        var api = new ApiEsportiva();


        var app1 = new SofaScore { Nome = "SofaScore" };
        var app2 = new Score365 { Nome = "365Score" };
        var app3 = new Betano { Nome = "Betano" };

        api.RegistrarObservador(app1);
        api.RegistrarObservador(app2);
        api.RegistrarObservador(app3);

        api.NotificarObservadores(new GolEventArgs
        {
            time = "Bahia",
            adversario = "Vitoria",
            jogador = "O Fantastico Eric Pulga"
        });

        api.RemoverObservador(app2);

        api.NotificarObservadores(new GolEventArgs
        {
            time = "Vasco",
            adversario = "Flamengo",
            jogador = "O Pirata Veggeti"
        });


    }

}