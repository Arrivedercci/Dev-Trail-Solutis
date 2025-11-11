using Api.Dtos.View;
using Api.Models;
using BankSystem.Api.Repositories;

namespace Api.Services;

public class ClienteService(IClienteRepository _clienteRepository) : IClienteService
{

    public async Task<ClienteView?> CreateClienteAsync(Cliente cliente)
    {
        await _clienteRepository.AddClienteAsync(cliente);

        return ClienteView.toClienteView(cliente);
    }
    public async Task<ClienteView?> GetClienteByCpfAsync(string cpf)
    {
        var cliente = await _clienteRepository.GetClienteByCpfAsync(cpf);
        if (cliente is null) return null;

        return ClienteView.toClienteView(cliente, cliente.Contas);
    }
    public async Task<ClienteView?> GetClienteByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetClienteByIdAsync(Guid.NewGuid());
        if (cliente is null) return null;

        return ClienteView.toClienteView(cliente, cliente.Contas);
    }

    public async Task<ClienteView?> UpdateClienteAsync(int id)
    {
        var cliente = await _clienteRepository.GetClienteByIdAsync(Guid.NewGuid());
        if (cliente is null) return null;

        await _clienteRepository.UpdateClienteAsync(cliente);

        return ClienteView.toClienteView(cliente, cliente.Contas);
    }
}
