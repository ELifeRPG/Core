using Mediator;

namespace ELifeRPG.Domain.Common;

public abstract class DomainEvent : INotification
{
    protected DomainEvent()
    {
        Occured = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }

    public DateTimeOffset Occured { get; protected set; }
}
