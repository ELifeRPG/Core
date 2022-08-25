using ELifeRPG.Domain.Banking.Internals;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking;

public enum BankAccountTransactionType
{
    CashDeposit,
    CashWithdrawal,
    BankTransfer,
}

public class BankAccountTransaction : EntityBase
{
    internal BankAccountTransaction()
    {
    }
    
    public BankAccountTransaction(BankAccount bankAccount, BankAccount target, decimal amount)
    {
        Type = BankAccountTransactionType.BankTransfer;
        BankAccount = bankAccount;
        Target = target;
        Amount = amount;
        Fees = new TransactionFee(this, bankAccount).Value;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public BankAccountTransactionType Type { get; init; }

    public BankAccount BankAccount { get; init; } = null!;

    public BankAccount? Target { get; init; }
    
    public decimal Amount { get; init; }
    
    public decimal Fees { get; init; }
}
