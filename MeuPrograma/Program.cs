using System;

public class EfetividadePokemon
{
    public void Efetividade()
    {
        Console.WriteLine("Por favor, digite um tipo Pokemon:");

        string tipo = (Console.ReadLine() ?? "").ToLowerInvariant();

        switch (tipo)
        {
            case "normal":
                Console.WriteLine("Causa 2x de dano em: (Nenhum)");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Pedra, Metal");
                Console.WriteLine("Não causa dano (0x) em: Fantasma");
                break;

            case "fogo":
                Console.WriteLine("Causa 2x de dano em: Planta, Gelo, Inseto, Metal");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Água, Pedra, Dragão");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "agua":
                Console.WriteLine("Causa 2x de dano em: Fogo, Terrestre, Pedra");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Água, Planta, Dragão");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "planta":
                Console.WriteLine("Causa 2x de dano em: Água, Terrestre, Pedra");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Planta, Venenoso, Voador, Inseto, Dragão, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "eletrico":
                Console.WriteLine("Causa 2x de dano em: Água, Voador");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Elétrico, Planta, Dragão");
                Console.WriteLine("Não causa dano (0x) em: Terrestre");
                break;

            case "gelo":
                Console.WriteLine("Causa 2x de dano em: Planta, Terrestre, Voador, Dragão");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Água, Gelo, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "lutador":
                Console.WriteLine("Causa 2x de dano em: Normal, Gelo, Pedra, Sombrio, Metal");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Venenoso, Voador, Psíquico, Inseto, Fada");
                Console.WriteLine("Não causa dano (0x) em: Fantasma");
                break;

            case "venenoso":
                Console.WriteLine("Causa 2x de dano em: Planta, Fada");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Venenoso, Terrestre, Pedra, Fantasma");
                Console.WriteLine("Não causa dano (0x) em: Metal");
                break;

            case "terrestre":
                Console.WriteLine("Causa 2x de dano em: Fogo, Elétrico, Venenoso, Pedra, Metal");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Planta, Inseto");
                Console.WriteLine("Não causa dano (0x) em: Voador");
                break;

            case "voador":
                Console.WriteLine("Causa 2x de dano em: Planta, Lutador, Inseto");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Elétrico, Pedra, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "psiquico":
                Console.WriteLine("Causa 2x de dano em: Lutador, Venenoso");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Psíquico, Metal");
                Console.WriteLine("Não causa dano (0x) em: Sombrio");
                break;

            case "inseto":
                Console.WriteLine("Causa 2x de dano em: Planta, Psíquico, Sombrio");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Lutador, Venenoso, Voador, Fantasma, Metal, Fada");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "pedra":
                Console.WriteLine("Causa 2x de dano em: Fogo, Gelo, Voador, Inseto");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Lutador, Terrestre, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "fantasma":
                Console.WriteLine("Causa 2x de dano em: Fantasma, Psíquico");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Sombrio");
                Console.WriteLine("Não causa dano (0x) em: Normal");
                break;

            case "dragao":
                Console.WriteLine("Causa 2x de dano em: Dragão");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Metal");
                Console.WriteLine("Não causa dano (0x) em: Fada");
                break;

            case "sombrio":
                Console.WriteLine("Causa 2x de dano em: Fantasma, Psíquico");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Lutador, Sombrio, Fada");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "metal":
                Console.WriteLine("Causa 2x de dano em: Gelo, Pedra, Fada");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Água, Elétrico, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            case "fada":
                Console.WriteLine("Causa 2x de dano em: Lutador, Dragão, Sombrio");
                Console.WriteLine("Causa 1/2x (0.5x) de dano em: Fogo, Venenoso, Metal");
                Console.WriteLine("Não causa dano (0x) em: (Nenhum)");
                break;

            default:
                Console.WriteLine($"Tipo '{tipo}' não reconhecido. Verifique a ortografia.");
                break;
        }
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        new EfetividadePokemon().Efetividade();
    }
}
