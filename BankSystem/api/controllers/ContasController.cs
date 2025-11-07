using Microsoft.AspNetCore.Mvc;
using Api.Dtos.Input;
using Api.Dtos.View;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;
using Api.Models;


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

            var cliente = await Cliente.GetClienteByCpf(_context, contaInput.CpfCliente);
            if (cliente is null) cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = contaInput.CpfCliente,
                Nome = contaInput.NomeCliente

            };

            var conta = contaInput.toConta(contaInput, cliente);
            cliente.Contas.Add(conta);
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();

            var view = ContaView.toContaView(conta);

            return CreatedAtAction(nameof(Get), new { conta.NumeroConta }, view);
        }

        [HttpGet(Name = "GetContas")]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Contas
                .AsNoTracking()
                .Include(c => c.Cliente)
                .Select(e => ContaView
                .toContaView(e, ClienteView.toClienteView(e.Cliente!)))
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{NumeroConta:int}", Name = "GetContaByNumero")]
        public async Task<IActionResult> Get(int NumeroConta)
        {
            var conta = await _context.Contas.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.NumeroConta == NumeroConta);
            if (conta is null) return NotFound();

            var view = ContaView.toContaView(conta, ClienteView.toClienteView(conta.Cliente!));

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
