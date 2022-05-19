using System.Reflection;
using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Infrastructure.Common;

#pragma warning disable CS8618

public class DatabaseContext : DbContext, IDatabaseContext
{
    private readonly IMediator _mediator;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Character> Characters { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var domainEvents = ChangeTracker.Entries<IIncludesDomainEvent>()
            .Select(x => x.Entity.DomainEvents).SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEvents(domainEvents);
        return result;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private async Task PublishDomainEvents(IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            domainEvent.IsPublished = true;
            await _mediator.Publish(domainEvent);
        }
    }
}
