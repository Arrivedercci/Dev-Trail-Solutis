using Api.Models;

namespace Api.Dtos.View
{
    public class ClienteView
    {

        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public List<ContaView> Contas { get; set; } = [];


        public static ClienteView toClienteView(Cliente cliente)
        {
            return new ClienteView
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf
            };
        }
        public static ClienteView toClienteView(Cliente cliente, List<Conta> contas)
        {
            return new ClienteView
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Contas = contas.Select(c => ContaView.toContaView(c)).ToList()
            };
        }



    }
}