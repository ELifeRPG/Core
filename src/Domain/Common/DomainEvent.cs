using MediatR;

namespace ELifeRPG.Domain.Common;

public interface IHasDomainEvents
{
    List<DomainEvent> DomainEvents { get; set; }
}

public abstract class DomainEvent : INotification
{
    protected DomainEvent()
    {
        Occured = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }

    public DateTimeOffset Occured { get; protected set; }
}
