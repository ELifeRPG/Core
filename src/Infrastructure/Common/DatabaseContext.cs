using System.Reflection;
using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
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
    
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Character> Characters { get; set; }
    
    public DbSet<Company> Companies { get; set; }
    
    public DbSet<CompanyMembership> CompanyMemberships { get; set; }
    
    public DbSet<CompanyPosition> CompanyPositions { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var domainEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .SelectMany(x => x.Entity.DomainEvents)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEvents(domainEvents);
        return result;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var types = typeof(Domain.Domain).Assembly.GetTypes();
        
        var typesDerivedFromIHasDomainEvents = types.Where(x => x.GetTypeInfo().ImplementedInterfaces.Any(type => type.GetTypeInfo().IsInterface && type == typeof(IHasDomainEvents)));
        foreach (var type in typesDerivedFromIHasDomainEvents)
        {
            builder.Entity(type).Ignore(nameof(IHasDomainEvents.DomainEvents));
        }
        
        var typesDerivedFromEntityBase = types.Where(x => x.GetTypeInfo().ImplementedInterfaces.Any(type => type.GetTypeInfo().IsClass && type == typeof(EntityBase)));
        foreach (var type in typesDerivedFromEntityBase)
        {
            builder.Entity(type).Property(nameof(EntityBase.Created)).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAdd();
        }
        
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
