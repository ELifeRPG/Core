using ELifeRPG.Domain.Banking.Internals;
using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

public enum BankAccountTransactionType
{
    CashDeposit,
    CashWithdrawal,
    BankTransfer,
}

public class BankAccountTransaction
{
    internal BankAccountTransaction()
    {
    }

    public BankAccountTransaction(BankAccount bankAccount, BankAccountTransactionType type, decimal amount)
    {
        if (type != BankAccountTransactionType.CashDeposit && type != BankAccountTransactionType.CashWithdrawal)
        {
            throw new ELifeInvalidOperationException();
        }
        
        Type = type;
        BankAccount = bankAccount;
        Amount = amount;
        Fees = new TransactionFee(this, bankAccount).Value;
    }
    
    public BankAccountTransaction(BankAccount bankAccount, BankAccount source, decimal amount)
    {
        Type = BankAccountTransactionType.BankTransfer;
        BankAccount = bankAccount;
        Source = source;
        Amount = amount;
        Fees = new TransactionFee(this, bankAccount).Value;
    }
    
    public BankAccountTransactionType Type { get; init; }

    public BankAccount BankAccount { get; init; } = null!;

    public BankAccount? Source { get; init; }
    
    public decimal Amount { get; init; }
    
    public decimal Fees { get; init; }
}
