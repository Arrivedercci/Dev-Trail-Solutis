using System;
public class Program
{
    public static void Main()
    {

        var api = new ApiEsportiva();


        var app1 = new XucoScore { Nome = "XucoScore" };
        var app2 = new Xuco365 { Nome = "365Xuco" };

        api.RegistrarObservador(app1);
        api.RegistrarObservador(app2);

        api.NotificarObservadores(new GolEventArgs
        {
            time = "Bahia",
            adversario = "Vitoria",
            jogador = "O Fantastico Eric Pulga"
        });
      

    }

}