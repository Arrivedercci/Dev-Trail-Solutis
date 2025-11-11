using Api.Dtos.View;
using Api.Models;

namespace Api.Services;

public interface IClienteService
{

    Task<ClienteView?> CreateClienteAsync(Cliente cliente);
    Task<ClienteView?> GetClienteByIdAsync(int id);
    Task<ClienteView?> GetClienteByCpfAsync(string cpf);
    Task<ClienteView?> UpdateClienteAsync(int id);

}