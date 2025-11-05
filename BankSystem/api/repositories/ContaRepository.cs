using System.Collections.Concurrent;
using Api.Dtos.View;

namespace BankSystem.Api.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ConcurrentDictionary<Guid, ContaView> _store = new();

        public void Add(ContaView conta) => _store[conta.NumeroConta] = conta;

        public ContaView? Get(Guid numero) => _store.TryGetValue(numero, out var c) ? c : null;

        public IEnumerable<ContaView> GetAll() => _store.Values;

        public void Update(ContaView conta) => _store[conta.NumeroConta] = conta;

        public void Delete(Guid numero) => _store.TryRemove(numero, out _);
    }
}