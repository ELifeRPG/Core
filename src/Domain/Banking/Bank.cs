using ELifeRPG.Domain.Banking.Events;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;

namespace ELifeRPG.Domain.Banking;

public class Bank : EntityBase, IHasDomainEvents
{
    private readonly List<BankAccount>? _accounts;

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Country Country { get; init; } = Country.Default;
    
    /// <summary>
    /// The national identification number of the bank.
    /// </summary>
    public int Number { get; init; }

    public IReadOnlyCollection<BankAccount>? Accounts
    {
        get => _accounts;
        init => _accounts = value!.ToList();
    }

    public BankAccount OpenAccount(Character owningCharacter)
    {
        var account = new BankAccount(this, owningCharacter);
        HandleNewAccount(account);
        return account;
    }
    
    public BankAccount OpenAccount(Company owningCompany)
    {
        var account = new BankAccount(this, owningCompany);
        HandleNewAccount(account);
        return account;
    }

    private void HandleNewAccount(BankAccount account)
    {
        _accounts!.Add(account);
        DomainEvents.Add(new BankAccountOpenedEvent(account));
    }

    public List<DomainEvent> DomainEvents { get; } = new();
}
