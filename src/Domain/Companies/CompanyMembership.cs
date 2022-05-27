using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Companies;

public class CompanyMembership : EntityBase
{
    public CompanyMembership()
    {
    }
    
    public CompanyMembership(Company company, Character character, CompanyPosition position)
    {
        Id = Guid.NewGuid();
        Company = company;
        Character = character;
        Position = position;
    }
    
    public Guid Id { get; init; }
    
    public Company Company { get; init; } = null!;
    
    public Character Character { get; init; } = null!;

    public CompanyPosition Position { get; init; } = null!;
}
