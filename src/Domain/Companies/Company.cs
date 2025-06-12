using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Persons;

namespace ELifeRPG.Domain.Companies;

public class Company : EntityBase, IHasDomainEvents
{
    public Company(string name)
    {
        Id = CompanyId.New().Value;
        Name = name;
        Positions = new List<CompanyPosition> { new() { Name = "Rookie", Ordering = 10, } };
        Memberships = new List<CompanyMembership>();
        Person = new Person();
    }

    internal Company()
    {
    }

    public Guid Id { get; init; }

    public Person? Person { get; init; }

    public string Name { get; init; } = null!;

    public ICollection<CompanyPosition>? Positions { get; init; }

    public ICollection<CompanyMembership>? Memberships { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public void AddMembership(Character character, CompanyPosition? position)
    {
        if (Memberships is null)
        {
            throw new InvalidOperationException("Memberships need to be loaded.");
        }

        position ??= Positions!.OrderByDescending(x => x.Ordering).First();

        Memberships.Add(new CompanyMembership(this, character, position));
    }
}
