using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Common;
using Mediator;
using Microsoft.Extensions.Configuration;

namespace ELifeRPG.Infrastructure.Common;

public sealed class ReadWriteDatabaseContext(IConfiguration configuration, IMediator mediator)
    : DatabaseContextBase(configuration.GetConnectionString("DatabaseReadWrite")), IReadWriteDatabaseContext
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .SelectMany(x => x.Entity.DomainEvents)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToList();

        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishDomainEvents(IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            domainEvent.IsPublished = true;
            await mediator.Publish(domainEvent);
        }
    }
}
