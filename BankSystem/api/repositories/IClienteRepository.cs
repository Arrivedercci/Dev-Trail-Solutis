using Api.Models;

namespace BankSystem.Api.Repositories;

public interface IClienteRepository
{
    public Task<Cliente?> GetClienteByCpfAsync(string cpf);
    public Task<Cliente?> GetClienteByIdAsync(Guid id);
    public Task AddClienteAsync(Cliente cliente);

    public Task UpdateClienteAsync(Cliente cliente);
}