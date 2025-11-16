namespace Api.Services
{
    using Api.Dtos.Input;
    using Api.Dtos.View;
    using Api.Models;
    using BankSystem.Api.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContaService(IContaRepository _contaRepository, IClienteRepository _clienteRepository) : IContaService
    {
        public async Task<ContaView?> GetContaByNumeroAsync(int numeroConta)
        {
            var conta = await _contaRepository.GetContaByNumeroAsync(numeroConta);
            var view = ContaView.toContaView(conta!, ClienteView.toClienteView(conta!.Cliente!));
            return view;
        }

        public async Task<ContaView?> GetContaByIdAsync(Guid id)
        {
            var conta = await _contaRepository.GetContaByIdAsync(id);
            var view = ContaView.toContaView(conta!);

            return view;
        }

        public async Task<IEnumerable<ContaView>> GetAllContasAsync()
        {
            var contas = await _contaRepository.GetAllContasAsync();
            if(contas is null) contas = new List<Conta>();
            var views = contas.Select(c => ContaView.toContaView(c!));
            return views;
        }

        public async Task<ContaView?> AddContaAsync(ContaInput contaInput)
        {
            var cliente = await _clienteRepository.GetClienteByCpfAsync(contaInput.CpfCliente);

            if (cliente is null)
            {
                cliente = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Cpf = contaInput.CpfCliente,
                    Nome = contaInput.NomeCliente
                };

                await _clienteRepository.AddClienteAsync(cliente);
            }

            var conta = contaInput.toConta(cliente!);
            cliente.Contas.Add(conta);
            await _contaRepository.AddContaAsync(conta);
            await _clienteRepository.UpdateClienteAsync(cliente!);

            var view = ContaView.toContaView(conta);
            return view;

        }

        public async Task<bool> DepositarAsync(Guid id, decimal valor)
        {
            var conta = await _contaRepository.GetContaByIdAsync(id);
            if (conta is null) return false;
            conta.Saldo += valor;
            await _contaRepository.UpdateContaAsync(conta);
            return true;
        }

        public async Task<bool> SacarAsync(Guid id, decimal valor)
        {
            var conta = await _contaRepository.GetContaByIdAsync(id);
            if (conta is null) return false;
            if (conta.Saldo < valor) return false;
            conta.Saldo -= valor;
            await _contaRepository.UpdateContaAsync(conta);
            return true;
        }

        public async Task<bool> DeleteContaAsync(Guid id)
        {
            var resultado = await _contaRepository.DeleteContaAsync(id);
            return resultado;
        }


    }
}