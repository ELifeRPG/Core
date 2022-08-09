using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;

namespace ELifeRPG.Domain.Banking;

public class BankAccount : EntityBase
{
    public BankAccount()
    {
    }
    
    public BankAccount(Bank bank, Character owningCharacter)
        : this(bank)
    {
        OwningCharacter = owningCharacter;
    }
    
    public BankAccount(Bank bank, Company owningCompany)
        : this(bank)
    {
        OwningCompany = owningCompany;
    }
    
    private BankAccount(Bank bank)
    {
        Bank = bank;
        Number = BankAccountNumberGenerator.Generate(Bank).Value;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The international <see cref="BankAccountNumber"/> (EBAN) of the bank account.
    /// </summary>
    public string Number { get; init; } = null!;

    public Bank Bank { get; init; } = null!;
    
    public Character? OwningCharacter { get; init; }
    
    public Company? OwningCompany { get; init; }

    public bool Can(Character character, BankAccountCapabilities capability)
    {
        if (OwningCharacter is not null)
        {
            return character.Id == OwningCharacter.Id;
        }

        var companyMembership = OwningCompany?.Memberships!.SingleOrDefault(x => x.Character.Id == character.Id);
        return companyMembership is not null && MapFromCompanyPosition(companyMembership.Position.Permissions).Contains(capability);
    }

    private static BankAccountCapabilities MapFromCompanyPosition(CompanyPermissions companyPermissions)
    {
        var capabilities = BankAccountCapabilities.None;

        if (companyPermissions.Contains(CompanyPermissions.ManageFinances))
        {
            capabilities |= BankAccountCapabilities.ViewTransactions | BankAccountCapabilities.CommitTransactions;
        }

        return capabilities;
    }
}
