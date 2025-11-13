using Microsoft.AspNetCore.Mvc;
using Api.Dtos.Input;
using BankSystem.Api.Repositories;
using Api.Services;


namespace BankSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController(IContaService _contaService) : ControllerBase
    {

        [HttpPost(Name = "CreateConta")]
        public async Task<IActionResult> CriarConta([FromBody] ContaInput contaInput)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var view = await _contaService.AddContaAsync(contaInput);

            return CreatedAtRoute("GetContaByNumero", new { numeroConta = view!.NumeroConta }, view);
        }

        [HttpGet(Name = "GetContas")]
        public async Task<IActionResult> GetContas()
        {
            var contas = await _contaService.GetAllContasAsync();
            return Ok(contas);
        }

        [HttpGet("{numeroConta:int}", Name = "GetContaByNumero")]
        public async Task<IActionResult> GetContaByNumero(int numeroConta)
        {
            var conta = await _contaService.GetContaByNumeroAsync(numeroConta);
            if (conta is null) return NotFound();
            return Ok(conta);
        }

        [HttpPatch("{id:guid}/deposito", Name = "Deposito")]
        public async Task<IActionResult> Depositar(Guid id, [FromBody] decimal valor)
        {
            var conta = await _contaService.GetContaByIdAsync(id);
            if (conta is null) return NotFound();

            var resultado = await _contaService.DepositarAsync(id, valor);
            if (!resultado) return BadRequest();


            return Ok(conta);

        }

        [HttpPatch("{id:guid}/saque", Name = "Saque")]
        public async Task<IActionResult> Sacar(Guid id, [FromBody] decimal valor)
        {
            var conta = await _contaService.GetContaByIdAsync(id);
            if (conta is null) return NotFound();

            var resultado = await _contaService.SacarAsync(id, valor);
            if (!resultado) return BadRequest();

            return Ok(conta);
        }

        [HttpDelete("{id:guid}", Name = "DeleteConta")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var result = await _contaService.DeleteContaAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
