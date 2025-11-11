namespace Api.Services
{
    using Api.Dtos.Input;
    using Api.Dtos.View;
    using Api.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContaService
    {
        Task<ContaView?> GetContaByNumeroAsync(int numeroConta);
        Task<ContaView?> GetContaByIdAsync(Guid id);
        Task<IEnumerable<ContaView>> GetAllContasAsync();
        Task<ContaView?> AddContaAsync(ContaInput contaInput);
        Task<bool> DepositarAsync(Guid id, decimal valor);
        Task<bool> SacarAsync(Guid id, decimal valor);
        Task<bool> DeleteContaAsync(Guid id);

    }
}