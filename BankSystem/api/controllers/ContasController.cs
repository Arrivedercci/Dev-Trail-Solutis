using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        List<Conta> contas = new List<Conta>()
        {
            new Conta(1, "João Silva", "Corrente", 1000m),
            new Conta(2, "Maria Oliveira", "Poupança", 5000m),
            new Conta(3, "Carlos Souza", "Especial", 2500m),
            new Conta(4, "Ana Santos", "Corrente", -3000m)

        };
        private readonly ILogger<ContasController> _logger;

        public ContasController(ILogger<ContasController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetContas")]
        public IEnumerable<Conta> Get()
        {
            return contas;
        }
    }
}