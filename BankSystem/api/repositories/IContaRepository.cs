using Api.Models.View;

namespace BankSystem.Api.Repositories
{
    public interface IContaRepository
    {
        void Add(ContaView conta);
        ContaView? Get(Guid numero);
        IEnumerable<ContaView> GetAll();
        void Update(ContaView conta);
        void Delete(Guid numero);
    }
}