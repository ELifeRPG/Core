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
    internal BankAccount()
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

    public Bank? Bank { get; init; }
    
    public BankCondition? BankCondition { get; init; }
    
    public Character? OwningCharacter { get; init; }
    
    public Company? OwningCompany { get; init; }

    public ICollection<BankAccountTransaction> SentTransactions { get; init; } = new List<BankAccountTransaction>();
    
    public ICollection<BankAccountTransaction> ReceivedTransactions { get; init; } = new List<BankAccountTransaction>();

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

    public BankAccountTransaction MakeTransactionTo(BankAccount targetAccount, Character character, decimal amount)
    {
        if (!Can(character, BankAccountCapabilities.CommitTransactions))
        {
            throw new ELifeInvalidOperationException();
        }
        
        var transaction = new BankAccountTransaction(this, targetAccount, amount);

        SentTransactions.Add(transaction);
        targetAccount.ReceivedTransactions.Add(transaction);

        DomainEvents.Add(new BankAccountTransactionExecutedEvent(transaction, character));

        return transaction;
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
