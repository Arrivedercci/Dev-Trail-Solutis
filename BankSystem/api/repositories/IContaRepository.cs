using Api.Dtos.View;
using Api.Models;

namespace BankSystem.Api.Repositories;

public interface IContaRepository
{
    Task<Conta?> GetContaByNumeroAsync(int numeroConta);
    Task<Conta?> GetContaByIdAsync(Guid id);
    Task<IEnumerable<Conta?>> GetAllContasAsync();
    Task AddContaAsync(Conta conta);
    Task UpdateContaAsync(Conta conta);
    Task<bool> DeleteContaAsync(Guid id);

}