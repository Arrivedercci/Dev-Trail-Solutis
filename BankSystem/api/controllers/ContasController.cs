using Microsoft.AspNetCore.Mvc;
using Api.Dtos.Input;
using Api.Dtos.View;
using BankSystem.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly BankContext _context;

        public ContasController(BankContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "CreateConta")]
        public async Task<IActionResult> Post([FromBody] ContaInput contaInput)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var conta = new Conta
            {
                Id = Guid.NewGuid(),
                NumeroConta = contaInput.NumeroConta,
                Saldo = contaInput.Saldo,
                Tipo = contaInput.Tipo,
                DataCriacao = DateTime.UtcNow,
                Status = contaInput.Status

            };

            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();

            var view = new ContaView
            {
                Id = conta.Id,
                NumeroConta = conta.NumeroConta,
                Saldo = conta.Saldo,
                Tipo = conta.Tipo,
                DataCriacao = conta.DataCriacao,
                Status = conta.Status
            };

            return CreatedAtAction(nameof(Get), new { conta.NumeroConta }, view);
        }
        [HttpGet(Name = "GetContas")]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Contas
                .AsNoTracking()
                .Select(e => new ContaView
                {
                    Id = e.Id,
                    NumeroConta = e.NumeroConta,
                    Saldo = e.Saldo,
                    Tipo = e.Tipo,
                    DataCriacao = e.DataCriacao,
                    Status = e.Status
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{NumeroConta:int}", Name = "GetContaByNumero")]
        public async Task<IActionResult> Get(int NumeroConta)
        {
            var conta = await _context.Contas.FirstOrDefaultAsync(c => c.NumeroConta == NumeroConta);
            if (conta is null) return NotFound();

            var view = new ContaView
            {
                Id = conta.Id,
                NumeroConta = conta.NumeroConta,
                Saldo = conta.Saldo,
                Tipo = conta.Tipo,
                DataCriacao = conta.DataCriacao,
                Status = conta.Status
            };

            return Ok(view);
        }

        [HttpPut("{id:guid}", Name = "Trasacao")]
        public async Task<IActionResult> Put(Guid id, [FromBody] decimal valor, string transacao)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta is null) return NotFound();

            if (valor <= 0m) return BadRequest("Valor deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(transacao)) return BadRequest("Tipo de transação obrigatório.");

            if (string.Equals(transacao, "saque", StringComparison.OrdinalIgnoreCase))
            {
                if (conta.Saldo < valor) return BadRequest("Saldo insuficiente.");
                var updated = new ContaView
                {
                    Id = conta.Id,
                    NumeroConta = conta.NumeroConta,
                    Saldo = conta.Saldo - valor,
                    Tipo = conta.Tipo,
                    DataCriacao = conta.DataCriacao,
                    Status = conta.Status

                };

                _context.Update(updated);
                return Ok(updated);
            }
            else if (string.Equals(transacao, "deposito", StringComparison.OrdinalIgnoreCase))
            {
                var updated = new ContaView
                {
                    Id = conta.Id,
                    NumeroConta = conta.NumeroConta,
                    Saldo = conta.Saldo + valor,
                    Tipo = conta.Tipo,
                    DataCriacao = conta.DataCriacao,
                    Status = conta.Status
                };

                _context.Update(updated);
                return Ok(updated);
            }

            return BadRequest("Tipo de transação inválido. Use 'saque' ou 'deposito'.");
        }


        [HttpDelete("{id:guid}", Name = "DeleteConta")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta is null) return NotFound();

            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
