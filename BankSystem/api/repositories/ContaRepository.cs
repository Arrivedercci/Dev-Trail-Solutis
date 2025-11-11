using Api.Models;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace BankSystem.Api.Repositories
{
    public class ContaRepository(BankContext _context) : IContaRepository
    {

        public async Task<Conta?> GetContaByNumeroAsync(int numeroConta)
        {
            var conta = await _context.Contas
            .Include(c => c.Cliente)
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

            return conta;
        }

        public async Task<Conta?> GetContaByIdAsync(Guid id)
        {
            var conta = await _context.Contas
                .Include(c => c.Cliente)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return conta;
        }

        public async Task<IEnumerable<Conta?>> GetAllContasAsync()
        {
            var contas = await _context.Contas
                .Include(c => c.Cliente)
                .ToListAsync();

            return contas;
        }

        public async Task AddContaAsync(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContaAsync(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteContaAsync(Guid id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta is null) return false;

            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}