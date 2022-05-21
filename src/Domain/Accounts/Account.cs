using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Accounts;

public class Account : EntityBase, IHasDomainEvents
{
    public Guid Id { get; init; }
    
    public ICollection<Character>? Characters { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();
}
