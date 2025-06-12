namespace ELifeRPG.Domain.Common;

public interface IHasDomainEvents
{
    List<DomainEvent> DomainEvents { get; }
}
