using ELifeRPG.Domain.Banking.Internals;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking;

public class BankAccountTransaction : EntityBase
{
    public BankAccountTransaction()
    {
    }
    
    public BankAccountTransaction(BankAccount source, BankAccount target, decimal amount)
    {
        Type = BankAccountTransactionType.BankTransfer;
        Source = source;
        Target = target;
        Amount = amount;
        Fees = new TransactionFee(this, source).Value;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public BankAccountTransactionType Type { get; init; }

    public BankAccount Source { get; init; } = null!;

    public BankAccount? Target { get; init; }
    
    public decimal Amount { get; init; }
    
    public decimal Fees { get; init; }
}
