using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking;

public enum BankAccountBookingType
{
    Incoming = 1,
    Outgoing = 2,
}

public class BankAccountBooking : EntityBase
{
    internal BankAccountBooking()
    {
    }

    public BankAccountBooking(BankAccount account, BankAccountTransaction transaction, string? purpose = null)
    {
        Type = GetType(account, transaction);
        BankAccount = account;
        Source = transaction.Source;
        Amount = GetAmount(Type, transaction);
        Purpose = purpose;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public BankAccountBookingType Type { get; init; }

    public BankAccount BankAccount { get; init; } = null!;

    public BankAccount? Source { get; init; }
    
    public string? Purpose { get; init; }
    
    public decimal Amount { get; init; }

    private static BankAccountBookingType GetType(BankAccount account, BankAccountTransaction transaction)
    {
        return transaction.Type switch
        {
            BankAccountTransactionType.CashDeposit => BankAccountBookingType.Incoming,
            
            BankAccountTransactionType.CashWithdrawal => BankAccountBookingType.Outgoing,
            
            BankAccountTransactionType.BankTransfer => 
                transaction.BankAccount.Id == account.Id
                    ? BankAccountBookingType.Outgoing
                    : BankAccountBookingType.Incoming,
            
            _ => throw new ArgumentOutOfRangeException(nameof(transaction), nameof(transaction.Type))
        };
    }

    private static decimal GetAmount(BankAccountBookingType type, BankAccountTransaction transaction)
    {
        return transaction.Type switch
        {
            BankAccountTransactionType.CashDeposit => transaction.Amount - transaction.Fees,
            
            BankAccountTransactionType.CashWithdrawal => transaction.Amount + transaction.Fees,
            
            BankAccountTransactionType.BankTransfer => type switch 
            { 
                BankAccountBookingType.Incoming => transaction.Amount,
                BankAccountBookingType.Outgoing => transaction.Amount + transaction.Fees,
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            },
            
            _ => throw new ArgumentOutOfRangeException(nameof(transaction), nameof(transaction.Type))
        };
    }
}
