using System.Globalization;

public class Transacao
{
    public Guid Id { get; set; }
    public int ContaNumero { get; set; }
    public decimal Valor { get; set; }

    public string Tipo { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; }



    public Transacao(Guid id, int contaNumero, decimal valor, string tipo, string descricao, DateTime data)
    {
        Id = id;
        ContaNumero = contaNumero;
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        Data = data;

    }
    public override string ToString()
    {
        var culture = new CultureInfo("pt-BR");
        return string.Format(culture, "{0} | {1} | {2:C} | {3}", Data, Tipo, Valor, Descricao);
    }

}
