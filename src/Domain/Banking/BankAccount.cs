using ELifeRPG.Domain.Banking.Events;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Common.Exceptions;
using ELifeRPG.Domain.Companies;

namespace ELifeRPG.Domain.Banking;

public enum BankAccountType
{
    Personal = 1,
    Corporate = 2,
}

public class BankAccount : EntityBase, IHasDomainEvents
{
    public BankAccount()
    {
    }
    
    public BankAccount(Bank bank, Character owningCharacter)
        : this(bank)
    {
        OwningCharacter = owningCharacter;
        Type = BankAccountType.Personal;
    }
    
    public BankAccount(Bank bank, Company owningCompany)
        : this(bank)
    {
        OwningCompany = owningCompany;
        Type = BankAccountType.Corporate;
    }
    
    private BankAccount(Bank bank)
    {
        Bank = bank;
        Number = new BankAccountNumber(Bank).Value;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public BankAccountType Type { get; init; }

    /// <summary>
    /// The international <see cref="BankAccountNumber"/> (EBAN) of the bank account.
    /// </summary>
    public string Number { get; init; } = null!;

    public Bank Bank { get; init; } = null!;
    
    public Character? OwningCharacter { get; init; }
    
    public Company? OwningCompany { get; init; }

    public decimal TransactionFeeMultiplier { get; init; } = 0.02m;

    public ICollection<BankAccountTransaction> Transactions { get; init; } = new List<BankAccountTransaction>();

    public List<DomainEvent> DomainEvents { get; } = new();

    public bool Can(Character character, BankAccountCapabilities capability)
    {
        if (Type == BankAccountType.Personal)
        {
            return character.Id == OwningCharacter!.Id;
        }

        var companyMembership = OwningCompany?.Memberships!.SingleOrDefault(x => x.Character.Id == character.Id);
        return companyMembership is not null && MapFromCompanyPosition(companyMembership.Position.Permissions).Contains(capability);
    }

    public void MakeTransactionTo(BankAccount targetAccount, Character character, decimal amount)
    {
        if (!Can(character, BankAccountCapabilities.CommitTransactions))
        {
            throw new ELifeInvalidOperationException();
        }
        
        var transaction = new BankAccountTransaction(this, targetAccount, amount);
        Transactions.Add(transaction);
        DomainEvents.Add(new BankAccountTransactionExecutedEvent(transaction, character));
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
