using Api.Models;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace BankSystem.Api.Repositories;

public class ClienteRepository(BankContext _context) : IClienteRepository
{
    public async Task<Cliente?> GetClienteByCpfAsync(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return null;
        var cliente = await _context.Clientes.Include(c => c.Contas).FirstOrDefaultAsync(c => c.Cpf == cpf);
        return cliente;
    }

    public async Task<Cliente?> GetClienteByIdAsync(Guid id)
    {
        if (id == Guid.Empty) return null;
        var cliente = await _context.Clientes.Include(c => c.Contas).FirstOrDefaultAsync(c => c.Id == id);
        return cliente;
    }

    public async Task UpdateClienteAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task AddClienteAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }
}