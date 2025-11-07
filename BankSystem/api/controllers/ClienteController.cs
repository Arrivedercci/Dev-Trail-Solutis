using Api.Dtos.View;
using Api.Models;
using BankSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly BankContext _context;

    public ClienteController(BankContext context)
    {
        _context = context;
    }

    [HttpPost(Name = "CreateCliente")]
    public async Task<IActionResult> Post([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByCPF), new { cpf = cliente.Cpf }, cliente);
    }

    [HttpGet("{cpf}", Name = "GetClienteByCPF")]
    public async Task<IActionResult> GetByCPF(string cpf)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        if (cliente is null) return NotFound();

        var contas = Cliente.GetClienteByCpf(_context, cpf).Result?.Contas ?? new List<Conta>();
        var view = ClienteView.toClienteView(cliente, contas);

        return Ok(view);
    }

}