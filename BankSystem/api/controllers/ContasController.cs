using Microsoft.AspNetCore.Mvc;
using Api.Models.Input;
using Api.Models.View;
using BankSystem.Api.Repositories;

namespace BankSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {

        private readonly ILogger<ContasController> _logger;
        private readonly IContaRepository _repo;

        public ContasController(ILogger<ContasController> logger, IContaRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpPost(Name = "CreateConta")]
        public IActionResult Post([FromBody] ContaInput contaInput)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var view = new ContaView
            {
                NumeroConta = Guid.NewGuid(),
                Titular = contaInput.Titular,
                Saldo = contaInput.SaldoInicial,
                Tipo = contaInput.Tipo
            };

            _repo.Add(view);
            return CreatedAtAction(nameof(Get), new { id = view.NumeroConta }, view);
        }

        [HttpGet(Name = "GetContas")]
        public IActionResult Get()
        {
            _logger.LogInformation("Obtendo a lista de contas.");
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id:guid}", Name = "GetContaById")]
        public IActionResult Get(Guid id)
        {
            var conta = _repo.Get(id);
            if (conta is null) return NotFound();
            return Ok(conta);
        }

        [HttpGet("{titular}", Name = "GetContaByTitular")]
        public IActionResult Get(string titular)
        {
            var conta = _repo.GetAll().FirstOrDefault(c => c.Titular == titular);
            if (conta is null) return NotFound();
            return Ok(conta);
        }

        [HttpPut("{id:guid}", Name = "Trasacao")]
        public IActionResult Put(Guid id, [FromBody] decimal valor, string transacao)
        {
            var conta = _repo.Get(id);
            if (conta is null) return NotFound();

            if (valor <= 0m) return BadRequest("Valor deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(transacao)) return BadRequest("Tipo de transação obrigatório.");

            if (string.Equals(transacao, "saque", StringComparison.OrdinalIgnoreCase))
            {
                if (conta.Saldo < valor) return BadRequest("Saldo insuficiente.");
                var updated = new ContaView
                {
                    NumeroConta = conta.NumeroConta,
                    Titular = conta.Titular,
                    Tipo = conta.Tipo,
                    Saldo = conta.Saldo - valor
                };

                _repo.Update(updated);
                return Ok(updated);
            }
            else if (string.Equals(transacao, "deposito", StringComparison.OrdinalIgnoreCase))
            {
                var updated = new ContaView
                {
                    NumeroConta = conta.NumeroConta,
                    Titular = conta.Titular,
                    Tipo = conta.Tipo,
                    Saldo = conta.Saldo + valor
                };

                _repo.Update(updated);
                return Ok(updated);
            }

            return BadRequest("Tipo de transação inválido. Use 'saque' ou 'deposito'.");
        }


        [HttpDelete("{id:guid}", Name = "DeleteConta")]
        public IActionResult Delete(Guid id)
        {
            var conta = _repo.Get(id);
            if (conta is null) return NotFound();

            _repo.Delete(id);
            return NoContent();
        }
    }
}
