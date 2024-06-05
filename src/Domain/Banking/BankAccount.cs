using System.Buffers;
using ELifeRPG.Domain.Banking.Events;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Common.Exceptions;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Persons;

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
    
    public BankAccount(Bank bank, Person owner)
        : this(bank)
    {
        Owner = owner;
        Type = owner.Character is null && owner.Company is not null ? BankAccountType.Corporate : BankAccountType.Personal;
    }
    
    private BankAccount(Bank bank)
    {
        Bank = bank;
        Number = new BankAccountNumber(Bank).Value;
        BankCondition = Bank.Conditions!.First();
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public BankAccountType Type { get; init; }

    /// <summary>
    /// The international <see cref="BankAccountNumber"/> (EBAN) of the bank account.
    /// </summary>
    public string Number { get; init; } = null!;

    public decimal Balance { get; set; }

    public Bank? Bank { get; init; }
    
    public BankCondition? BankCondition { get; init; }

    public Person Owner { get; init; } = null!;

    public ICollection<BankAccountBooking>? Bookings { get; init; }

    public List<DomainEvent> DomainEvents { get; } = new();

    public bool Can(Person person, BankAccountCapabilities capability)
    {
        if (Type == BankAccountType.Personal)
        {
            return person.Character!.Id == Owner.Character!.Id;
        }

        var companyMembership = Owner.Company!.Memberships!.SingleOrDefault(x => x.Character.Id == person.Id);
        return companyMembership is not null && MapFromCompanyPosition(companyMembership.Position.Permissions).Contains(capability);
    }

    /// <summary>
    /// Tries to execute a transaction to the destination bank-account.
    /// </summary>
    /// <param name="targetAccount">The destination bank-account.</param>
    /// <param name="amount">The amount to be transferred, without fees.</param> 
    /// <param name="character">The executing person.</param>
    /// <returns>The transaction including fees.</returns>
    /// <exception cref="ELifeInvalidOperationException">Throws if the character is not allowed to execute the transaction.</exception>
    public BankAccountTransaction TransferMoneyTo(BankAccount targetAccount, decimal amount, Character? character)
    {
        if (Bookings is null || targetAccount.Bookings is null)
        {
            throw new InvalidOperationException();
        }
        
        if (character is not null && !Can(character.Person!, BankAccountCapabilities.CommitTransactions))
        {
            throw new ELifeInvalidOperationException();
        }
        
        var transaction = new BankAccountTransaction(this, targetAccount, amount);
        var booking = new BankAccountBooking(this, transaction);
        
        if (Balance < booking.Amount)
        {
            throw new ELifeInvalidOperationException("Can not withdraw money due to low account-balance.");
        }
        
        Bookings.Add(booking);
        Balance -= booking.Amount;

        var targetAccountBooking = new BankAccountBooking(targetAccount, transaction);
        targetAccount.Bookings.Add(targetAccountBooking);
        targetAccount.Balance += targetAccountBooking.Amount;

        DomainEvents.Add(new BankAccountTransactionExecutedEvent(transaction, character));

        return transaction;
    }

    /// <summary>
    /// Tries to withdraw money from a bank-account, eg. for ATM's.
    /// </summary>
    /// <param name="character">The executing character.</param>
    /// <param name="amount">The amount to be withdrawn, without fees.</param>
    /// <returns>The transaction including fees.</returns>
    /// <exception cref="ELifeInvalidOperationException">Throws if the character is not allowed to execute the transaction.</exception>
    public BankAccountTransaction WithdrawMoney(Character character, decimal amount)
    {
        if (Bookings is null)
        {
            throw new InvalidOperationException();
        }
        
        if (!Can(character.Person!, BankAccountCapabilities.CommitTransactions))
        {
            throw new ELifeInvalidOperationException();
        }
        
        var transaction = new BankAccountTransaction(this, BankAccountTransactionType.CashWithdrawal, amount);
        var booking = new BankAccountBooking(this, transaction);

        if (Balance < booking.Amount)
        {
            throw new ELifeInvalidOperationException("Can not withdraw money due to low account-balance.");
        }
        
        Bookings.Add(booking);
        Balance -= booking.Amount;
        
        DomainEvents.Add(new BankAccountTransactionExecutedEvent(transaction, character));

        return transaction;
    }
    
    public (BankAccountTransaction Transaction, BankAccountBooking Booking) DepositMoney(decimal amount)
    {
        if (Bookings is null)
        {
            throw new InvalidOperationException();
        }

        var transaction = new BankAccountTransaction(this, BankAccountTransactionType.CashDeposit, amount);
        var booking = new BankAccountBooking(this, transaction);
        Bookings.Add(booking);
        Balance += booking.Amount;

        DomainEvents.Add(new BankAccountTransactionExecutedEvent(transaction));

        return (transaction, booking);
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
