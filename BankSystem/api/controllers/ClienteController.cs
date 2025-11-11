using Api.Dtos.View;
using Api.Models;
using Api.Services;
using BankSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController(IClienteService _clienteService) : ControllerBase
{
    [HttpPost(Name = "CreateCliente")]
    public async Task<IActionResult> Post([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _clienteService.CreateClienteAsync(cliente);
        return CreatedAtAction(nameof(GetByCPF), new { cpf = cliente.Cpf }, cliente);
    }

    [HttpGet("{cpf}", Name = "GetClienteByCPF")]
    public async Task<IActionResult> GetByCPF(string cpf)
    {
        var clienteView = await _clienteService.GetClienteByCpfAsync(cpf);
        if (clienteView == null) return NotFound();

        return Ok(clienteView);
    }

}